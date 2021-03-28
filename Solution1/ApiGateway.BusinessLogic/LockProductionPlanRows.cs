using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateway.BusinessLogic.Interfaces;
using DatabaseModelling.DbModels;
using DatabaseModelling.DbModels.LockData.ProductionPlanLock;
using EnergyBidding.Shared.Documents.ProductionPlanXml;
using Interfaces;

namespace ApiGateway.BusinessLogic
{
    public class LockProductionPlanRows : ILockProductionPlanRows
    {
        public IDataBase<ProductionPlan, Guid> DbProductionPlanConnection { get; set; }
        public IDataBase<ProductionPlanColumn, int> DbProductionPlanColumn { get; set; }
        public LockProductionPlanRows(IDataBase<ProductionPlan, Guid> dbProductionPlanConnection, IDataBase<ProductionPlanColumn, int> dbProductionPlanColumn)
        {
            DbProductionPlanConnection = dbProductionPlanConnection;
            DbProductionPlanColumn = dbProductionPlanColumn;
        }


        public async Task<bool> SaveLockProductionPlan(Company company, Area area, DateTime planDate)
        {
            List<ProductionPlan> ProductionPlans = await DbProductionPlanConnection.ReadAsync(x => x.Company.PublicIdentifier == company.PublicIdentifier 
                                                          && x.Area.PublicIdentifier == area.PublicIdentifier 
                                                          && x.Date == planDate);
            int lastVersion = ProductionPlans.Max(x => x.Version);
            ProductionPlan productionPlan = ProductionPlans.FirstOrDefault(x => x.Version == lastVersion);
            OperationalScheduleDocument productionPlanDocument = await 
                EnergyBidding.Shared.XmlReader.ReadRawBidXml<OperationalScheduleDocument>(productionPlan.XmlString);
            DateTime date = DateTime.UtcNow;
            date = date.AddHours(2).AddMinutes(5);
            if (date.Day != DateTime.UtcNow.AddHours(2).Day)
            {
                return false;
            }
            int interval = 5;
            int LockRows = (date.Hour * 60 + date.Minute) / interval;
            List<ProductionPlanColumn> lockColumn = await DbProductionPlanColumn.ReadAsync(
                x => x.ProductionPlanPublicIdentifier == productionPlan.PublicIdentifier);
            foreach (var ProductionPlanColumn in productionPlanDocument.OperationalScheduleTimeSeries)
            {
                ProductionPlanColumn productionPlanColumn =
                    lockColumn.FirstOrDefault(x => x.CollumName.Equals(ProductionPlanColumn.UnitIdentification.V));
                if (productionPlanColumn != null)
                {
                    for (int i = 1; i <= LockRows; i++)
                    {
                        if (productionPlanColumn.Rows.FirstOrDefault(z => z.Index == i) == null)
                        {
                            var documentcell =
                                ProductionPlanColumn.Period.Interval.FirstOrDefault(x => x.Position.V == i);
                            productionPlanColumn.Rows.Add(new ProductionPlanCell { Index =i, Quantity = documentcell.Quantity.V});
                        }
                    }
                    await DbProductionPlanColumn.UpdateAsync(productionPlanColumn);

                }
                else
                {
                    productionPlanColumn  = new ProductionPlanColumn
                    {
                        Id = 0,
                        Rows = new List<ProductionPlanCell>(),
                        ProductionPlanPublicIdentifier = productionPlan.PublicIdentifier,
                        CollumName = ProductionPlanColumn.UnitIdentification.V
                    };
                    for (int i = 0; i < LockRows; i++)
                    {
                        var celle = ProductionPlanColumn.Period.Interval.FirstOrDefault(y => y.Position.V == (i + 1));
                        productionPlanColumn.Rows.Add(new ProductionPlanCell { Index = i, Quantity = celle.Quantity.V });
                    }
                    await DbProductionPlanColumn.CreateAsync(productionPlanColumn);

                }
            }
            return true;
        }
    }
}