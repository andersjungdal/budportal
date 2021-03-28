using Interfaces;
using RawDatabaseModelling.Models;
using System;
using System.Threading.Tasks;

namespace RawDatabaseModelling.CRUD
{
    public class ModelleringAktions : IDataBase<Aktion>
    {
        public Task<bool> Cread(Aktion obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Cread(Aktion[] obj)
        {
            throw new NotImplementedException();
        }

        public Task Delate(Action<Aktion> sech)
        {
            throw new NotImplementedException();
        }

        public Task<Aktion[]> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Aktion[]> ReadAsync(Action<Aktion> sech)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReplaceAllAsync(Action<Aktion> sech, Aktion obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Aktion obj)
        {
            throw new NotImplementedException();
        }
    }
}