using System.IO;
using System.Threading.Tasks;
using DatabaseModelling.DbModels;
using ModelsInterfaces;

namespace ApiGateway.BusinessLogic.Interfaces
{
    public interface IXmlReader
    {
        Task<bool> PopulateRawBid(IRawBid<Area, User, Company, XmlTemplate> rawBid);
        Task<bool> PopulateProductionPlan(IProductionPlan<Area, User, Company, XmlTemplate> productionPlan);
    }
}