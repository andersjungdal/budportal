using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DatabaseModelling.Context;
using DatabaseModelling.DbModels;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DatabaseModelling.CRUD
{
    public class ModellingCompany : IDataBase<Company, Guid>
    {
        public SecurityDbContext SecurityDbContext { get; set; }

        public ModellingCompany(SecurityDbContext securityDbContext)
        {
            SecurityDbContext = securityDbContext;
        }
        public async Task CreateAsync(Company[] obj)
        {
            
            foreach (Company company in obj)
            {
                company.Zone = SecurityDbContext.Zones.FirstOrDefault(x =>
                    x.City.Equals(company.City) && x.ZipCode.Equals(company.ZipCode)) ?? company.Zone;
                company.Roaden = SecurityDbContext.Roads.FirstOrDefault(x =>
                    x.Road.Equals(company.Road)) ?? company.Roaden;

                await SecurityDbContext.Companies.AddAsync(company);
                await SecurityDbContext.SaveChangesAsync();
                if (company.RawBidTemplate == null)
                {
                    company.RawBidTemplate = (await SecurityDbContext.Companies.FindAsync(1)).RawBidTemplate;
                    company.ProductionPlanTemplate = (await SecurityDbContext.Companies.FindAsync(1)).ProductionPlanTemplate;
                    SecurityDbContext.Companies.Update(company);
                    await SecurityDbContext.SaveChangesAsync();
                }
            }
        }

        public async Task CreateAsync(Company company)
        {
            company.Zone = SecurityDbContext.Zones.FirstOrDefault(x =>
                x.City.Equals(company.City) && x.ZipCode.Equals(company.ZipCode)) ?? company.Zone;
            company.Roaden = SecurityDbContext.Roads.FirstOrDefault(x =>
                x.Road.Equals(company.Road)) ?? company.Roaden;

            await SecurityDbContext.Companies.AddAsync(company);
            await SecurityDbContext.SaveChangesAsync();
            if (company.RawBidTemplate != null && company.ProductionPlanTemplate != null)
            {
                return;
            }
            if (company.RawBidTemplate == null)
            {
                company.RawBidTemplate = (await SecurityDbContext.Companies.FindAsync(1)).RawBidTemplate;
            }
            if (company.ProductionPlanTemplate == null )
            {
                company.ProductionPlanTemplate = (await SecurityDbContext.Companies.FindAsync(1)).ProductionPlanTemplate;
                
            }
            SecurityDbContext.Companies.Update(company);
            await SecurityDbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid search)
        {
            SecurityDbContext.Companies.Remove(await SecurityDbContext.Companies.FirstOrDefaultAsync(x => x.PublicIdentifier== search));
            await SecurityDbContext.SaveChangesAsync();
        }

        public async Task<List<Company>> ReadAllAsync()
        {
            var Tabel = SecurityDbContext.Companies
                .Include(x => x.Zone)
                .Include(x => x.Roaden);

            return Tabel.ToList();
        }

        public async Task<List<Company>> ReadAsync(Func<Company, bool> search)
        {
            var Tabel = SecurityDbContext.Companies
                .Include(x => x.ProductionPlanTemplate)
                .Include(x => x.RawBidTemplate)
                .Include(x => x.Zone)
                .Include(x => x.Roaden);
            return (await Tabel.ToListAsync()).FindAll(x => search(x));
        }

        public async Task UpdateAsync(Company obj)
        {

            Company oldCompany = await SecurityDbContext.Companies
                .Include(x=>x.Roaden)
                .Include(x=>x.Zone)
                .FirstOrDefaultAsync(x => x.PublicIdentifier == obj.PublicIdentifier);

            if (!oldCompany.Road.Equals(obj.Road))
            {
                oldCompany.Roaden = SecurityDbContext.Roads.FirstOrDefault(x=>x.Road.Equals(obj.Road)) ?? obj.Roaden;
            }
            if (!(oldCompany.ZipCode.Equals(obj.ZipCode)&&oldCompany.City.Equals(obj.City)))
            {
                oldCompany.Zone = SecurityDbContext.Zones.FirstOrDefault(x=>x.ZipCode.Equals(obj.ZipCode) && x.City.Equals(obj.City)) ?? obj.Zone;
            }
            oldCompany.Name = obj.Name ?? oldCompany.Name;
            oldCompany.StreetNumber = obj.StreetNumber ?? oldCompany.StreetNumber;
             SecurityDbContext.Companies.Update(oldCompany);
             await SecurityDbContext.SaveChangesAsync();
        }
    }
}