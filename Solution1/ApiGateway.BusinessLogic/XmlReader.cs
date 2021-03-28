using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ApiGateway.BusinessLogic.Interfaces;
using DatabaseModelling.DbModels;
using EnergyBidding.Shared.Documents.ProductionPlanXml;
using EnergyBidding.Shared.Documents.XmlDocument;
using Interfaces;
using ModelsInterfaces;

namespace ApiGateway.BusinessLogic
{
    public class XmlReader : IXmlReader
    {
        public IDataBase<Area, Guid> AreaDatabase { get; set; }
        public IDataBase<Company, Guid> CompanyDatabase { get; set; }
        public IDataBase<RawBid, Guid> RawBidDataBase { get; set; }
        public IDataBase<ProductionPlan, Guid> ProductionPlanDataBase { get; set; }
        //TODO Chage Name
        public XmlReader(IDataBase<Area, Guid> areaDatabase,
            IDataBase<Company, Guid> companyDatabase, IDataBase<RawBid,Guid> rawBidDataBase, IDataBase<ProductionPlan,Guid>productionPlanDataBase)
        {
            AreaDatabase = areaDatabase;
            CompanyDatabase = companyDatabase;
            RawBidDataBase = rawBidDataBase;
            ProductionPlanDataBase = productionPlanDataBase;
        }
        public async Task<bool> PopulateRawBid(IRawBid<Area, User, Company, XmlTemplate> rawBid)
        {
            BidDocument xmlDocument;
            rawBid.PublicIdentifier = Guid.NewGuid();
            xmlDocument= await EnergyBidding.Shared.XmlReader.ReadRawBidXml<BidDocument>(rawBid.XmlString);
            if (xmlDocument == null)
            {
                return false;
            }
            DateTime time = xmlDocument.MessageHeader.DocumentDateTime.v;
            rawBid.Date = new DateTime(time.Year,time.Month, time.Day);
            rawBid.Company = (await CompanyDatabase.ReadAsync(x => x.XmlIdentifier == (long)xmlDocument.MessageHeader.SenderIdentification.v))[0];
            if (rawBid.Company == null)
            {
                return false;
            }
            rawBid.Area = (await AreaDatabase.ReadAsync(x =>xmlDocument.MessageHeader.Domain.v.Contains(x.Type)))[0];
            if (rawBid.Area == null)
            {
                return false;
            }
            List<RawBid> oldDocument = await RawBidDataBase.ReadAsync(x =>
                x.Date.Equals(rawBid.Date) && x.Area.Id == rawBid.Area.Id && x.Company.Id == rawBid.Company.Id);
            if (oldDocument.Count > 0)
            {
                rawBid.PublicIdentifier= oldDocument[0].PublicIdentifier;
            }
            rawBid.Version = oldDocument.Count+1;
            return true;
        }
        public async Task<bool> PopulateProductionPlan(IProductionPlan<Area, User, Company, XmlTemplate> productionPlan)
        {
            OperationalScheduleDocument xmlDocument;
            productionPlan.PublicIdentifier = Guid.NewGuid();

            XmlSerializer serializer = new XmlSerializer(typeof(OperationalScheduleDocument));
            using (TextReader reader = new StringReader(productionPlan.XmlString))
            {
                xmlDocument = (OperationalScheduleDocument)serializer.Deserialize(reader);
            }
            DateTime time = xmlDocument.MessageHeader.DocumentDateTime.v;
            productionPlan.Date = new DateTime(time.Year,time.Month, time.Day);
           
            productionPlan.Company = (await CompanyDatabase.ReadAsync(x => x.XmlIdentifier == (long)xmlDocument.MessageHeader.SenderIdentification.v))[0];
            if (productionPlan.Company == null)
            {
                return false;
            }
            productionPlan.Area = (await AreaDatabase.ReadAsync(x =>xmlDocument.MessageHeader.Domain.v.Contains(x.Type)))[0];
            if (productionPlan.Area == null)
            {
                return false;
            }
            return true;
        }
    }
}