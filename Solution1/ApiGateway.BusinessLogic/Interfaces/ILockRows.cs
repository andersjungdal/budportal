using System;
using System.Threading.Tasks;
using DatabaseModelling.DbModels;

namespace ApiGateway.BusinessLogic.Interfaces
{
    public interface ILockRows
    {
        Task<bool> SaveLockRawBid(Company company, Area area, DateTime date);
    }
}