using System;
using System.Threading.Tasks;
using DatabaseModelling.DbModels;

namespace ApiGateway.BusinessLogic.Interfaces
{
    public interface ILockProductionPlanRows
    {
        Task<bool> SaveLockProductionPlan(Company company, Area area, DateTime planDate);
    }
}