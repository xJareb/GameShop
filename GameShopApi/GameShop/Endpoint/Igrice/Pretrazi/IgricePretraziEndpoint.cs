using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Igrice.Pretrazi
{
    [Tags("Igrice")]
    public class IgricePretraziEndpoint : MyBaseEndpoint<IgricePretraziRequest, IgricePretraziResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public IgricePretraziEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("Pretrazi")]
        public override async Task<IgricePretraziResponse> Obradi([FromQuery]IgricePretraziRequest request, CancellationToken cancellationToken = default)
        {
            var igrice = await _applicationDbContext.Igrice.Where(i=>request.IgricaID == i.Id || request.IgricaID == 0).Select(x => new IgricePretraziResponseIgrice()
            {
                Id = x.Id,
                Naziv = x.Naziv,
                Zanr = x.Zanr.Naziv,
                DatumIzlaska = x.DatumIzlaska,
                Slika = x.Slika,
                Izdavac = x.Izdavac,
                Opis = x.Opis,
                Cijena = x.Cijena,
                PostotakAkcije = x.PostotakAkcije,
                AkcijskaCijena = x.AkcijskaCijena ?? 0
            }).ToListAsync();

            return new IgricePretraziResponse
            {
                Igrice = igrice
            };
        }
    }
}
