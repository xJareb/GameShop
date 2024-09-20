using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Igrice.Ponuda
{
    [Tags("Igrice")]
    public class IgricePonudaEndpoint : MyBaseEndpoint<NoRequest, IgricePonudaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IgricePonudaEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("IzdvojeneIgrice")]
        public override async Task<IgricePonudaResponse> Obradi([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            var izdvojeneIgrice = await _applicationDbContext.Igrice.Where(i => i.Izdvojeno == true).Select(x => new IgricePonudaResponseIgrice()
            {
                Id = x.Id,
                Naziv = x.Naziv,
                Slika = x.Slika,
                AkcijskaCijena = x.AkcijskaCijena,
                PostotakAkcije = x.PostotakAkcije
            }).ToListAsync();

            return new IgricePonudaResponse()
            {
                igrice = izdvojeneIgrice
            };
        }
    }
}
