using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ApiGateway.BusinessLogic.Interfaces;
using DatabaseModelling.DbModels;
using DatabaseModelling.DbModels.LockData.ProductionPlanLock;
using DatabaseModelling.DbModels.LockData.RawBidLock;
using EnergyBidding.Shared.Documents.ProductionPlanXml;
using EnergyBidding.Shared.Documents.XmlDocument;
using Interfaces;
using EnergyBidding.Shared;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace ApiGateway.BusinessLogic
{
    public class LockRows : ILockRows
    {
        public IDataBase<RawBid, Guid> DBRawBidConnection { get; set; }
        public IDataBase<RawBidColumn, int> DbRawBidColumn { get; set; }

        public LockRows(IDataBase<RawBid, Guid> dBRawBidConnection,IDataBase<RawBidColumn, int> dbRawBidColumn)
        {
            DBRawBidConnection = dBRawBidConnection;
            DbRawBidColumn = dbRawBidColumn;
        }
        //TODO: DST
        public async Task<bool> SaveLockRawBid(Company company, Area area, DateTime bidDate)
        {
            List<RawBid> docs = await DBRawBidConnection.ReadAsync(x => x.Company.PublicIdentifier == company.PublicIdentifier && x.Area.PublicIdentifier == area.PublicIdentifier && x.Date == bidDate);
            int lastVersion = docs.Max(x => x.Version);
            RawBid doc = docs.FirstOrDefault(x => x.Version == lastVersion);
            Guid PublicId = doc.PublicIdentifier;
            BidDocument RawBidDokunent = await EnergyBidding.Shared.XmlReader.ReadRawBidXml<BidDocument>(doc.XmlString);
            DateTime date = DateTime.UtcNow;
            date = date.AddHours(2).AddMinutes(5);
            if (date.Day != DateTime.UtcNow.AddHours(2).Day)
            {
                return false;
            }
            int Intaval = 60;
            int LockRows = (date.Hour*60 + date.Minute)/ Intaval;                
            List<RawBidColumn> lockColumn = (await DbRawBidColumn.ReadAsync(x => x.RawBidPublicIdentifier == PublicId));

            foreach (var BidCollums in RawBidDokunent.BidMessage)
            {   
                RawBidColumn rawBidColumn = lockColumn.FirstOrDefault(x => x.CollumName.Equals(BidCollums.BidIdentification.v));
                if (rawBidColumn != null)
                {
                    for (int i = 1; i <= LockRows; i++)
                    {
                        if (rawBidColumn.Rows.FirstOrDefault(x => x.Index == i) == null)
                        {
                            var Documentcelle = BidCollums.Period.Interval.FirstOrDefault(x => x.Position.v == i);
                            rawBidColumn.Rows.Add(new RawBidCell { Index = i, Prize = Documentcelle.Price.v, Quantity = Documentcelle.Quantity.v });
                        }
                    }
                    await DbRawBidColumn.UpdateAsync(rawBidColumn);
                }
                else
                {
                    rawBidColumn = new RawBidColumn{Id = 0, RawBidPublicIdentifier = PublicId,CollumName = BidCollums.BidIdentification.v, Rows = new List<RawBidCell>()};
                    for (int i = 0; i < LockRows; i++)
                    {
                        var celle = BidCollums.Period.Interval.FirstOrDefault(x => x.Position.v == (i + 1));
                        rawBidColumn.Rows.Add(new RawBidCell{ Index = i+1, Prize = celle.Price.v,Quantity  = celle.Quantity.v});
                    }
                    await DbRawBidColumn.CreateAsync(rawBidColumn);
                }
            }
            return true;
        }
    }
}
