using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseModelling.Context;
using DatabaseModelling.DbModels;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatabaseModelling.CRUD
{
    public class ModellingArea : IDataBase<Area,Guid>
    {
        public SecurityDbContext Security { get; set; }

        public ModellingArea(SecurityDbContext security)
        {
            Security = security;
        }
        public async Task CreateAsync(Area[] obj)
        {
            await Security.Areas.AddRangeAsync(obj);
            await Security.SaveChangesAsync();
        }

        public async Task CreateAsync(Area obj)
        {
            await Security.Areas.AddAsync(obj);
            await Security.SaveChangesAsync();
        }

        public async Task Delete(Guid search)
        {
            Security.Areas.Remove(await Security.Areas.FirstOrDefaultAsync(x =>x.PublicIdentifier == search));
            await Security.SaveChangesAsync();
        }
        
        public async Task<List<Area>> ReadAllAsync()
        {
            return Security.Areas.ToList();
        }

        public async Task<List<Area>> ReadAsync(Func<Area, bool> search)
        {
            return Security.Areas.ToList().FindAll(x => search(x));
        }

        public async Task UpdateAsync(Area obj)
        {
            Area oldArea = await Security.Areas.FirstOrDefaultAsync(x => x.PublicIdentifier == obj.PublicIdentifier);
            oldArea.Type = obj.Type ?? oldArea.Type;
            oldArea.Description = obj.Description ?? oldArea.Description;
            Security.Areas.Update(oldArea);
            await Security.SaveChangesAsync();
        }
    }
}