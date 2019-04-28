using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WWTravelClubREST.DTOs;

namespace WWTravelClubREST.Controllers
{
    [Route("api/packages")]
    [ApiController]
    
    public class PackagesController : ControllerBase
    {
        [HttpGet("bydate/{start}/{stop}")]
        [ProducesResponseType(typeof(IEnumerable<PackagesListDTO>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetPackagesByDate(
            [FromServices] WWTravelClubDB.MainDBContext ctx,  
            DateTime start, DateTime stop)
        {
try
{
    var res = await ctx.Packages
        .Where(m => start >= m.StartValidityDate
        && stop <= m.EndValidityDate)
        .Select(m => new PackagesListDTO
        {
            StartValidityDate = m.StartValidityDate,
            EndValidityDate = m.EndValidityDate,
            Name = m.Name,
            DuratioInDays = m.DuratioInDays,
            Id = m.Id,
            Price = m.Price,
            DestinationName = m.MyDestination.Name,
            DestinationId = m.DestinationId
        })
        .ToListAsync();
    return Ok(res);
}
catch
{
    return StatusCode(500);
}
        }
    }
}