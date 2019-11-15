using DDD.DomainLayer;
using PackagesManagementDomain.Aggregates;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PackagesManagementDB.Models
{
    public class Destination: Entity<int>, IDestination
    {
        
        [MaxLength(128), Required]
        public string Name { get; set; }
        [MaxLength(128), Required]
        public string Country { get; set; }
        public string Description { get; set; }
        public ICollection<Package> Packages { get; set; }

        public void FullUpdate(IDestination o)
        {
            if (IsTransient())
            {
                Id = o.Id;
            }
            Name = o.Name;
            Country = o.Country;
            Description = o.Description;
        }
    }
}
