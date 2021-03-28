using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseModelling.Context;
using DatabaseModelling.DbModels;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DatabaseModelling.CRUD
{
    public class ModellingProductionPlan : IDataBase<ProductionPlan, Guid>
    {
        public SecurityDbContext SecurityDbContext { get; set; }

        public ModellingProductionPlan(SecurityDbContext securityDbContext)
        {
            SecurityDbContext = securityDbContext;
        }
        public async Task CreateAsync(ProductionPlan[] obj)
        {
            foreach (ProductionPlan productionPlan in obj)
            {
                productionPlan.Area = await SecurityDbContext.Areas.FirstOrDefaultAsync(x => x.PublicIdentifier == productionPlan.Area.PublicIdentifier)?? productionPlan.Area;
                productionPlan.Company = await SecurityDbContext.Companies.FirstOrDefaultAsync(x => x.Id == productionPlan.Company.Id)?? productionPlan.Company;
            }
            await SecurityDbContext.ProductionPlans.AddRangeAsync(obj);
            await SecurityDbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(ProductionPlan obj)
        {
            var test = (await ReadAsync(x =>
            {
                return ((x.Company.PublicIdentifier.Equals(obj.Company.PublicIdentifier))
                        && x.Date.Year == obj.Date.Year && x.Date.Month == obj.Date.Month &&
                        x.Date.Day == obj.Date.Day &&
                        (x.Area.PublicIdentifier.Equals(obj.Area.PublicIdentifier)));
            }));
            if (test.DefaultIfEmpty() == null || test.Count == 0)
            {
                obj.Area = await SecurityDbContext.Areas.FirstOrDefaultAsync(x => x.PublicIdentifier == obj.Area.PublicIdentifier) ?? obj.Area;
                obj.Company = await SecurityDbContext.Companies.FirstOrDefaultAsync(x => x.PublicIdentifier == obj.Company.PublicIdentifier) ?? obj.Company;
                obj.User = await SecurityDbContext.Users.FindAsync(obj.User.Id);
                obj.Version = 1;
            }
            else
            {
                ProductionPlan oldProductionPlan = test.OrderByDescending(xx => xx.Version).First();
                obj.Version = oldProductionPlan.Version+1;
                obj.PublicIdentifier = oldProductionPlan.PublicIdentifier;
            }
            await SecurityDbContext.ProductionPlans.AddAsync(obj);
            await SecurityDbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid search)
        {
            SecurityDbContext.ProductionPlans.Remove(await SecurityDbContext.ProductionPlans.FirstOrDefaultAsync(x => x.PublicIdentifier == search));
            await SecurityDbContext.SaveChangesAsync();
        }

        public async Task<List<ProductionPlan>> ReadAllAsync()
        {
            return await SecurityDbContext.ProductionPlans.ToListAsync();
        }

        public async Task<List<ProductionPlan>> ReadAsync(Func<ProductionPlan, bool> search)
        {
            var Tabel = SecurityDbContext.ProductionPlans
                .Include(x => x.Area)
                .Include(x => x.Company).ThenInclude(x=>x.Zone)
                .Include(x => x.Company).ThenInclude(x=>x.Roaden)
                .Include(x => x.User).ThenInclude(x => x.Company).ThenInclude(x => x.Zone)
                .Include(x => x.User).ThenInclude(x => x.Company).ThenInclude(x => x.Roaden);
            return (await Tabel.ToListAsync()).FindAll(x => search(x));
        }

        public async Task UpdateAsync(ProductionPlan obj)
        {
            ProductionPlan oldProductionPlan = SecurityDbContext.ProductionPlans
                .Include(x => x.Area)
                .Include(x => x.Company).ThenInclude(x=>x.Zone)
                .Include(x => x.Company).ThenInclude(x=>x.Roaden)
                .Include(x=>x.User)
                .Where(x => x.Date.Equals(obj.Date))
                .Where(x => x.Company.PublicIdentifier.Equals(obj.Company.PublicIdentifier))
                .Where(x => x.Area.PublicIdentifier.Equals(obj.Area.PublicIdentifier))
                .ToList().OrderByDescending(x=>x.Version).First();
            if (obj.Area != null)
            {
                oldProductionPlan.Area = await SecurityDbContext.Areas.FirstOrDefaultAsync(x => x.PublicIdentifier == obj.Area.PublicIdentifier) ?? oldProductionPlan.Area;
            }

            if (obj.Company != null)
            {
                oldProductionPlan.Company = await SecurityDbContext.Companies.FirstOrDefaultAsync(x => x.PublicIdentifier == obj.Company.PublicIdentifier) ?? oldProductionPlan.Company;
            }

            oldProductionPlan.Id = 0;
            oldProductionPlan.User = await SecurityDbContext.Users.FirstOrDefaultAsync(x => x.Id == obj.User.Id);
            oldProductionPlan.XmlString = obj.XmlString;
            oldProductionPlan.Version++;
            Console.WriteLine(oldProductionPlan.PublicIdentifier);
            SecurityDbContext.Add(oldProductionPlan);
            await SecurityDbContext.SaveChangesAsync();
        }
    }
}