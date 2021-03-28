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
    public class ModellingRawBit : IDataBase<RawBid, Guid>
    {
        public SecurityDbContext SecurityDbContext { get; set; }

        public ModellingRawBit(SecurityDbContext securityDbContext)
        {
            SecurityDbContext = securityDbContext;
        }
        public async Task CreateAsync(RawBid[] obj)
        {
            foreach (RawBid rawBid in obj)
            {
                rawBid.Area = await SecurityDbContext.Areas.FirstOrDefaultAsync(x => x.PublicIdentifier == rawBid.Area.PublicIdentifier)?? rawBid.Area;
                rawBid.Company = await SecurityDbContext.Companies.FirstOrDefaultAsync(x => x.Id == rawBid.Company.Id)?? rawBid.Company;
            }
            await SecurityDbContext.RawBids.AddRangeAsync(obj);
            await SecurityDbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(RawBid obj)
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
                RawBid oldBid = test.OrderByDescending(xx => xx.Version).First();
                obj.Version = oldBid.Version+1;
                obj.PublicIdentifier = oldBid.PublicIdentifier;
            }
            await SecurityDbContext.RawBids.AddAsync(obj);
            await SecurityDbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid search)
        {
            SecurityDbContext.RawBids.Remove(await SecurityDbContext.RawBids.FirstOrDefaultAsync(x => x.PublicIdentifier == search));
            await SecurityDbContext.SaveChangesAsync();
        }

        public async Task<List<RawBid>> ReadAllAsync()
        {
            return await SecurityDbContext.RawBids.ToListAsync();
        }

        public async Task<List<RawBid>> ReadAsync(Func<RawBid, bool> search)
        {
            var Tabel = SecurityDbContext.RawBids
                .Include(x => x.Area)
                .Include(x => x.User).ThenInclude(x => x.Company).ThenInclude(x=>x.Zone)
                .Include(x => x.User).ThenInclude(x => x.Company).ThenInclude(x=>x.Roaden)
                .Include(x => x.Company).ThenInclude(x => x.Zone)
                .Include(x => x.Company).ThenInclude(x => x.Roaden);
            return (await Tabel.ToListAsync()).FindAll(x => search(x));
        }

        public async Task UpdateAsync(RawBid obj)
        {
            RawBid oldBid = SecurityDbContext.RawBids
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
                oldBid.Area = await SecurityDbContext.Areas.FirstOrDefaultAsync(x => x.PublicIdentifier == obj.Area.PublicIdentifier) ?? oldBid.Area;
            }

            if (obj.Company != null)
            {
                oldBid.Company = await SecurityDbContext.Companies.FirstOrDefaultAsync(x => x.PublicIdentifier == obj.Company.PublicIdentifier) ?? oldBid.Company;
            }

            oldBid.Id = 0;
            oldBid.User = await SecurityDbContext.Users.FirstOrDefaultAsync(x => x.Id == obj.User.Id);
            oldBid.XmlString = obj.XmlString;
            oldBid.Version++;
            SecurityDbContext.Add(oldBid);
            await SecurityDbContext.SaveChangesAsync();
        }
    }
}