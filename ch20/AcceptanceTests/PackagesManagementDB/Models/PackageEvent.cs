using DDD.DomainLayer;
using PackagesManagementDomain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;

namespace PackagesManagementDB.Models
{
    public class PackageEvent: Entity<long>, IPackageEvent
    {
        public PackageEventType Type { get; set; }
        public int PackageId { get; set; }
        public decimal NewPrice { get; set; }
        public long? OldVersion { get; set; }
        public long? NewVersion { get; set; }
    }
}
