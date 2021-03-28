using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseModelling.Context;
using DatabaseModelling.DbModels;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelsInterfaces.Enums;

namespace DatabaseModelling.CRUD
{
    public class ModellingUser : IDataBase<User,Guid>
    {
        public SecurityDbContext DbContext { get; set; }

        public ModellingUser(SecurityDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<List<User>> ReadAllAsync()
        {
            return await DbContext.Users.ToListAsync();
        }

        public async Task<List<User>> ReadAsync(Func<User, bool> search)
        {
            return (await DbContext.Users
                .Include(x => x.Company).ThenInclude(x => x.Zone)
                .Include(x => x.Company).ThenInclude(x => x.Roaden)
                .ToListAsync()).FindAll(x => search(x));
        }

        public async Task UpdateAsync(User obj)
        {
            User OldUser = DbContext.Users.Find(obj.Id);
            OldUser.PasswordHash = obj.PasswordHash ?? OldUser.PasswordHash;
            OldUser.SecurityStamp = obj.SecurityStamp ?? OldUser.SecurityStamp;
            OldUser.UserName = obj.UserName ?? OldUser.UserName;
            OldUser.Role = obj.Role == Role.NonAuthorized ? OldUser.Role: obj.Role;
            if (obj.Company != null)
            {
                OldUser.Company = await DbContext.Companies.FirstOrDefaultAsync(x => x.Id == obj.Company.Id) ?? OldUser.Company;
            }

            DbContext.Users.Update(OldUser);
            await DbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(User obj)
        {
            obj.Company = await DbContext.Companies.FirstOrDefaultAsync(x => x.PublicIdentifier == obj.Company.PublicIdentifier);

            await DbContext.Users.AddAsync(obj);
            await DbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(User[] obj)
        {
            foreach (User user in obj)
            {
                user.Company = await DbContext.Companies.FirstOrDefaultAsync(x => x.PublicIdentifier == user.Company.PublicIdentifier);
            }
            await DbContext.Users.AddRangeAsync(obj);
            await DbContext.SaveChangesAsync();
        }
        public async Task Delete(Guid search)
        {
            DbContext.Users.Remove(await DbContext.Users.FindAsync(search));
            await DbContext.SaveChangesAsync();
        }
    }
}