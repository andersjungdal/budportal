using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseModelling.Context;
using DatabaseModelling.DbModels;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace DatabaseModelling.CRUD
{
    public class ModellingXmlTemplate : IDataBase<XmlTemplate, Guid>
    {
        public SecurityDbContext Security { get; set; }

        public ModellingXmlTemplate(SecurityDbContext security)
        {
            Security = security;
        }
        public Task CreateAsync(XmlTemplate obj)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(XmlTemplate[] obj)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Guid search)
        {
            Security.Areas.Remove(await Security.Areas.FirstOrDefaultAsync(x => x.PublicIdentifier == search));
            await Security.SaveChangesAsync();
        }

        public Task<List<XmlTemplate>> ReadAllAsync()
        {
            return Security.XmlTemplates.ToListAsync();
        }

        public async Task<List<XmlTemplate>> ReadAsync(Func<XmlTemplate, bool> search)
        {
            return (await Security.XmlTemplates.ToListAsync()).FindAll(x => search(x));
        }

        public async Task UpdateAsync(XmlTemplate obj)
        {
            XmlTemplate oldXmlTemplate = await Security.XmlTemplates.FirstOrDefaultAsync(x => x.PublicIdentifire == obj.PublicIdentifire);
            oldXmlTemplate.XMLTemplate = obj.XMLTemplate ?? oldXmlTemplate.XMLTemplate;
            Security.XmlTemplates.Update(oldXmlTemplate);
            await Security.SaveChangesAsync();
        }
    }
}