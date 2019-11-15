using DDD.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackagesManagementDomain.Events
{
    public class PackageDeleteEvent: IEventNotification
    {
        public PackageDeleteEvent(int id, long oldVersion)
        {
            PackageId = id;
            OldVersion = oldVersion;
        }
        public int PackageId { get; private set; }
        public long OldVersion { get; private set; }
        
    }
}
