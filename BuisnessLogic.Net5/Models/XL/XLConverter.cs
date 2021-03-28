using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EnergyBidding.Shared.Documents.ProductionPlanXml;
using EnergyBidding.Shared.Documents.XmlDocument;
using Syncfusion.XlsIO;

namespace BlazorBuisnessLogic.Net5.Models.XL
{
    public class XLConverter
    {
        private static readonly char[] letters = new char[26]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
            'V', 'W', 'X', 'Y', 'Z'
        };

        public static IWorkbook ConvertBidToExcel(BidDocument bidDocument, IApplication application)
        {
            //this is the walue to change the time interval
            int Interval = 60;

            IWorkbook workbook = application.Workbooks.Create(1);
            IWorksheet worksheet = workbook.Worksheets[0];
            int columnIndex = 1;
            //TODO: DST
            for (int index = 0; index < (1440 / Interval); index++)
            {
                worksheet.Range["A" + (index + 3)].Text = new TimeSpan(0, index * Interval, 0).ToString(@"hh\:mm") +
                                                          " - " + new TimeSpan(0, (index + 1) * Interval, 0)
                                                              .ToString(@"hh\:mm").Replace("00:00", "24:00");
            }

            //This describes the column letter
            for (int i = 0; i < bidDocument.BidMessage.Count; i++)
            {
                worksheet.Range[GennerateCollum(columnIndex) + "1"].Text =
                    bidDocument.BidMessage[i].BidIdentification.v;
                string mergesCollums = GennerateCollum(columnIndex) + "1:" + GennerateCollum(columnIndex + 1) + "1";
                worksheet.Range[mergesCollums].Merge();

                worksheet.Range[GennerateCollum(columnIndex) + "2"].Text = "Quantity";
                worksheet.Range[GennerateCollum(columnIndex + 1) + "2"].Text = "Price";
                foreach (var interval in bidDocument.BidMessage[i].Period.Interval)
                {
                    worksheet.Range[GennerateCollum(columnIndex) + (2 + interval.Position.v)].Number =
                        interval.Quantity.v;
                    worksheet.Range[GennerateCollum(columnIndex + 1) + (2 + interval.Position.v)].Number =
                        interval.Price.v;
                }

                columnIndex += 2;
            }

            return workbook;
        }

        public static IWorkbook ConvertProductionPlanToExcel(OperationalScheduleDocument operationalScheduleDocument, IApplication application)
        {
            int Interval = 5;

            IWorkbook workbook = application.Workbooks.Create(1);
            IWorksheet worksheet = workbook.Worksheets[0];
            int columnIndex = 1;
            for (int index = 0; index < (1440/Interval); index++)
            {
                worksheet.Range["A"+(index+3)].Text = new TimeSpan(0, index*Interval,0).ToString(@"hh\:mm")+
                            " - " + new TimeSpan(0,(index+1)*Interval, 0)
                            .ToString(@"hh\:mm").Replace("00:00", "24:00");
            }

            for (int i = 0; i < operationalScheduleDocument.OperationalScheduleTimeSeries.Count; i++)
            {
                worksheet.Range[GennerateCollum(columnIndex) + "1"].Text =
                    operationalScheduleDocument.OperationalScheduleTimeSeries[i].TimeSeriesIdentification.V;
                worksheet.Range[GennerateCollum(columnIndex)+"2"].Text= "Quantity";
                foreach (var interval in operationalScheduleDocument.OperationalScheduleTimeSeries[i].Period.Interval)
                {
                    worksheet.Range[GennerateCollum(columnIndex) + (2 + interval.Position.V)].Text =
                        interval.Quantity.V.ToString();
                }

                columnIndex++;
            }

            return workbook;
        }

        private static string GennerateCollum(int collunumber)
        {
            string cullumName = "";
            while (collunumber > 0)
            {
                int letter1 = collunumber % letters.Length;
                collunumber -= letter1;
                cullumName = letters[letter1].ToString() + cullumName;
                if (collunumber != 0)
                {
                    collunumber /= letters.Length;
                }
            }

            return cullumName;
        }

        public static BidDocument ConvertExcelToBid(Stream XlSteam, BidDocument bidDocument)
        {
            XlSteam.Seek(0, SeekOrigin.Begin);
            Console.WriteLine(XlSteam.Length);
            BidDocument BidResult = new BidDocument {MessageHeader = bidDocument.MessageHeader};
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                //Initialize application
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Open(XlSteam, ExcelOpenType.Automatic);
                IWorksheet worksheet = workbook.Worksheets[0];
                List<BidDocumentBidMessage> Message = bidDocument.BidMessage.ToList();
                Console.WriteLine(worksheet.Columns.Length);
                for (int c = 2; c+1 <= worksheet.Columns.Length; c += 2)
                {
                    int currentInterval = (c - 2) / 2;
                    if (Message.Count < currentInterval)
                    {
                        BidDocumentBidMessage message = new BidDocumentBidMessage()
                        {
                            BidIdentification = new BidDocumentBidIdentification{v = string.IsNullOrEmpty(worksheet.Range[GennerateCollum(c)+1].Value)? worksheet.Range[GennerateCollum(c)+1].Value: worksheet.Range[GennerateCollum(c+1)+1].Value },
                            ContractIdentification =new BidDocumentContractIdentification {v = Message[0].ContractIdentification.v},
                            BusinessType = new BidDocumentBidMessageBusinessType(){v = Message[0].BusinessType.v}, 
                            MeasurementUnitQuantity = new BidDocumentBidMessageMeasurementUnitQuantity{v = Message[0].MeasurementUnitQuantity.v},
                            MeasurementUnitPrice = new BidDocumentBidMessageMeasurementUnitPrice{v = Message[0].MeasurementUnitPrice.v},
                            Currency = new BidDocumentBidMessageCurrency{v=Message[0].Currency.v},
                            StartGradient = new BidDocumentBidMessageStartGradient{v = Message[0].StartGradient.v},
                            StopGradient = new BidDocumentBidMessageStopGradient{v = Message[0].StopGradient.v},
                            DeadTime = new BidDocumentBidMessageDeadTime{v = Message[0].DeadTime.v},
                            Period = new BidDocumentBidMessagePeriod
                            {
                                BidInterval = new BidDocumentBidMessagePeriodBidInterval{v = Message[0].Period.BidInterval.v},
                                Resolution = new BidDocumentBidMessagePeriodResolution{v = Message[0].Period.Resolution.v}
                            }
                        };
                        Message.Add(message);
                    }
                    List<BidDocumentBidMessagePeriodInterval> intervals = new List<BidDocumentBidMessagePeriodInterval>();
                    for (int r = 3; r <= worksheet.Rows.Length; r++)
                    {
                        Console.WriteLine(GennerateCollum(c)+r+ ": " + worksheet.Range[r, c].Value);
                        double.TryParse(worksheet.Range[GennerateCollum(c)+r].Value,out double price);
                        double.TryParse(worksheet.Range[GennerateCollum(c) + r].Value,out double quentity);
                        intervals.Add(new BidDocumentBidMessagePeriodInterval
                        {
                            Position = new BidDocumentBidMessagePeriodIntervalPosition{v=(byte)(r-2)},
                            Price = new BidDocumentBidMessagePeriodIntervalPrice{v = price },
                            Quantity = new BidDocumentBidMessagePeriodIntervalQuantity{v = quentity }
                        });
                    }
                    Message[currentInterval].Period.Interval = intervals.ToArray();
                }

                BidResult.BidMessage = Message;
            }
            return BidResult;
        }
    }
}