using DDD.DomainLayer;
using PackagesManagementDomain.Aggregates;
using PackagesManagementDomain.DTOs;
using PackagesManagementDomain.Events;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PackagesManagementDB.Models
{
    public class Package: Entity<int>, IPackage
    {
        public void FullUpdate(IPackageFullEditDTO o)
        {
            if (IsTransient())
            {
                Id = o.Id;
                DestinationId = o.DestinationId;
            }
            else
            {
                if (o.Price != this.Price)
                    this.AddDomainEvent(new PackagePriceChangedEvent(
                            Id, o.Price, EntityVersion, EntityVersion+1));
            }
            Name = o.Name;
            Description = o.Description;
            Price = o.Price;
            DuratioInDays = o.DuratioInDays;
            StartValidityDate = o.StartValidityDate;
            EndValidityDate = o.EndValidityDate;
        }
        [MaxLength(128), Required]
        public string Name { get; set; }
        [MaxLength(128)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DuratioInDays { get; set; }
        public DateTime? StartValidityDate { get; set; }
        public DateTime? EndValidityDate { get; set; }
        public Destination MyDestination { get; set; }
        [ConcurrencyCheck]
        public long EntityVersion{ get; set; }

        public int DestinationId { get; set; }
    }
}
