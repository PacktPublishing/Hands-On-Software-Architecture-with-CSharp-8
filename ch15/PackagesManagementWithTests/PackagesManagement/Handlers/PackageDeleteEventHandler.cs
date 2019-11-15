using DDD.ApplicationLayer;
using PackagesManagementDomain.Events;
using PackagesManagementDomain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PackagesManagementDomain.Aggregates;

namespace PackagesManagement.Handlers
{
    public class PackageDeleteEventHandler :
        IEventHandler<PackageDeleteEvent>
    {
        IPackageEventRepository repo;
        public PackageDeleteEventHandler(IPackageEventRepository repo)
        {
            this.repo = repo;
        }
        public async Task HandleAsync(PackageDeleteEvent ev)
        {
            repo.New(PackageEventType.Deleted, ev.PackageId, ev.OldVersion);
        }
    }
}
