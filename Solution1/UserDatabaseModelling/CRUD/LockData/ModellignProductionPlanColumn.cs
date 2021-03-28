using Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DatabaseModelling.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DatabaseModelling.DbModels.LockData.ProductionPlanLock;

namespace DatabaseModelling.CRUD.LockData
{
    public class ModellignProductionPlanColumn : IDataBase<ProductionPlanColumn, int>
    {
        public SecurityDbContext Security { get; set; }
        public ModellignProductionPlanColumn(SecurityDbContext security)
        {
            Security = security;
        }
        public async Task CreateAsync(ProductionPlanColumn obj)
        {
            await Security.ProductionPlanColumns.AddAsync(obj);
            await Security.SaveChangesAsync();
        }

        public async Task CreateAsync(ProductionPlanColumn[] obj)
        {
            await Security.ProductionPlanColumns.AddRangeAsync(obj);
            await Security.SaveChangesAsync();
        }

        public Task Delete(int search)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductionPlanColumn>> ReadAllAsync()
        {
            var productionPlan = Security.ProductionPlanColumns
                .Include(x => x.Rows);
            return await productionPlan.ToListAsync();
        }

        public async Task<List<ProductionPlanColumn>> ReadAsync(Func<ProductionPlanColumn, bool> search)
        {
            var productionPlan = Security.ProductionPlanColumns
                .Include(x => x.Rows);
            return (await productionPlan.ToListAsync()).FindAll(x => search(x));
        }

        public async Task UpdateAsync(ProductionPlanColumn obj)
        {
            ProductionPlanColumn oldProductionPlanColumn = await Security.ProductionPlanColumns
                .Include(x => x.Rows)
                .FirstOrDefaultAsync(x => x.Id == obj.Id);
            foreach (var cells in obj.Rows)
            {
                if (oldProductionPlanColumn.Rows
                    .FirstOrDefault(x => x.Index == cells.Index) == null)
                {

                    cells.ProductionPlanColumn = oldProductionPlanColumn;
                    cells.ColumnId = oldProductionPlanColumn.Id;
                    Security.ProductionPlanCells.Add(cells);

                }
            }
            Security.ProductionPlanColumns.Update(oldProductionPlanColumn);
            await Security.SaveChangesAsync();
        }
    }
}