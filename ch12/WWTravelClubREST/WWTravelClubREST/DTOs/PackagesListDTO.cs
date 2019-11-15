using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WWTravelClubREST.DTOs
{
    public class PackagesListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DuratioInDays { get; set; }
        public DateTime? StartValidityDate { get; set; }
        public DateTime? EndValidityDate { get; set; }
        public string DestinationName { get; set; }
        public int DestinationId { get; set; }
    }
}
