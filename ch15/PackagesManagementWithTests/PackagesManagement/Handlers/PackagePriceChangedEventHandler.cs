using DDD.ApplicationLayer;
using PackagesManagementDomain.Aggregates;
using PackagesManagementDomain.Events;
using PackagesManagementDomain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackagesManagement.Handlers
{
    public class PackagePriceChangedEventHandler :
        IEventHandler<PackagePriceChangedEvent>
    {
        IPackageEventRepository repo;
        public PackagePriceChangedEventHandler(IPackageEventRepository repo)
        {
            this.repo = repo;
        }
        public async Task HandleAsync(PackagePriceChangedEvent ev)
        {
            repo.New(PackageEventType.CostChanged, ev.PackageId, ev.OldVersion, ev.NewVersion, ev.NewPrice);
        }
    }
}
