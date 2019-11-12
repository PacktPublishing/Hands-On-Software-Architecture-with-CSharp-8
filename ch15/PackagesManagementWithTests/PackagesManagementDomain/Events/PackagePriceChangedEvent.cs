using DDD.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackagesManagementDomain.Events
{
    public class PackagePriceChangedEvent: IEventNotification
    {
        public PackagePriceChangedEvent(int id, decimal price, long oldVersion, long newVersion)
        {
            PackageId = id;
            NewPrice = price;
            OldVersion = oldVersion;
            NewVersion = newVersion;
        }
        public int PackageId { get; private set; }
        public decimal NewPrice { get; private set; }
        public long OldVersion { get; private set; }
        public long NewVersion { get; private set; }
    }
}
