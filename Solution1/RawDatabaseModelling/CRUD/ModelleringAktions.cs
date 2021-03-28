using Interfaces;
using RawDatabaseModelling.Models;
using System;
using System.Threading.Tasks;

namespace RawDatabaseModelling.CRUD
{
    public class ModelleringRawBid : IDataBase<RawBid>
    {
        public Task<bool> Cread(RawBid obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Cread(RawBid[] obj)
        {
            throw new NotImplementedException();
        }

        public Task Delate(Action<RawBid> sech)
        {
            throw new NotImplementedException();
        }

        public Task<RawBid[]> ReadAsync(Action<RawBid> sech)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReplaceAllAsync(Action<RawBid> sech, RawBid obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(RawBid obj)
        {
            throw new NotImplementedException();
        }

        Task<RawBid[]> IDataBase<RawBid>.ReadAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}