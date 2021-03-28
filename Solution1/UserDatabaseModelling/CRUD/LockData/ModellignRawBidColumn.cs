using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModelling.Context;
using DatabaseModelling.DbModels.LockData.RawBidLock;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatabaseModelling.CRUD.LockData
{
    public class ModellignRawBidColumn : IDataBase<RawBidColumn, int>
    {
        public SecurityDbContext SecurityDbContext;

        public ModellignRawBidColumn(SecurityDbContext securityDbContext)
        {
            SecurityDbContext = securityDbContext;
        }
        public Task<List<RawBidColumn>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<RawBidColumn>> ReadAsync(Func<RawBidColumn, bool> search)
        {
           return SecurityDbContext.RawBidColumns.Include(x=>x.Rows)
                .ToList().FindAll(x => search(x));
        }

        public async Task UpdateAsync(RawBidColumn obj)
        {
            RawBidColumn oldRawBidColumn = await SecurityDbContext.RawBidColumns
                .FirstOrDefaultAsync(x => x.Id == obj.Id);
            
            foreach (var cells in obj.Rows)
            {
                if (SecurityDbContext.RawBidCells.FirstOrDefault( x => x.RawBidColumnId == oldRawBidColumn.Id &&  x.Index == cells.Index)==null)
                {
                    cells.rawBidColumn = oldRawBidColumn;
                    cells.RawBidColumnId = oldRawBidColumn.Id;
                    SecurityDbContext.RawBidCells.Add(cells);
                }
            }

            SecurityDbContext.RawBidColumns.Update(oldRawBidColumn);
            await SecurityDbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(RawBidColumn obj)
        {
            await SecurityDbContext.AddAsync(obj);
            await SecurityDbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(RawBidColumn[] obj)
        {
            await SecurityDbContext.AddRangeAsync(obj);
            await SecurityDbContext.SaveChangesAsync();
        }

        public Task Delete(int search)
        {
            throw new NotImplementedException();
        }
    }
}
