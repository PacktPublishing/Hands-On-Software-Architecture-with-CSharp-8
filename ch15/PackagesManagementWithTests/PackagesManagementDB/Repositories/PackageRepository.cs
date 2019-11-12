using DDD.DomainLayer;
using PackagesManagementDomain.Aggregates;
using PackagesManagementDomain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PackagesManagementDB.Models;
using PackagesManagementDomain.Events;

namespace PackagesManagementDB.Repositories
{
    public class PackageRepository : IPackageRepository
    {
        private MainDBContext context;
        public PackageRepository(MainDBContext context)
        {
            this.context = context;
        }
        public IUnitOfWork UnitOfWork => context;

        public async Task<IPackage> Get(int id)
        {
            return await context.Packages.Where(m => m.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<IPackage> Delete(int id)
        {
            var model = await Get(id);
            if (model == null) return null;
            context.Packages.Remove(model as Package);
            model.AddDomainEvent(
                new PackageDeleteEvent(
                    model.Id, (model as Package).EntityVersion));
            return model;
        }
        public IPackage New()
        {
            var model = new Package() {EntityVersion=1 };
            context.Packages.Add(model);
            return model;
        }
    }
}
