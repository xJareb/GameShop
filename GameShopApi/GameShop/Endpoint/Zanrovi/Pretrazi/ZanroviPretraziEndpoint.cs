using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Zanrovi.Pretrazi
{
    [Tags("Zanr")]
    public class ZanroviPretraziEndpoint : MyBaseEndpoint<NoRequest, ZanroviPretraziResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ZanroviPretraziEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("Izlistaj")]
        public override async Task<ZanroviPretraziResponse> Obradi([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            var zanrovi = await _applicationDbContext.Zanr.Select(x => new ZanroviPretraziResponseZanr()
            {
                ID = x.Id,
                Naziv = x.Naziv
            }).ToListAsync();

            return new ZanroviPretraziResponse()
            {
                Zanrovi = zanrovi
            };
        }
    }
}
