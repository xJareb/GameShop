using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Igrice.Kategorisi
{
    [Tags("Igrice")]
    public class IgriceKategorisiEndpoint : MyBaseEndpoint<IgriceKategorisiRequest,IgriceKategorisiResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IgriceKategorisiEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("ByKategorija")]
        public override async Task<IgriceKategorisiResponse> Obradi([FromQuery]IgriceKategorisiRequest request, CancellationToken cancellationToken = default)
        {
            var igrice = await _applicationDbContext.Igrice.Where(i => (i.ZanrID == request.ZanrID || request.ZanrID == 0) && (i.AkcijskaCijena > request.PocetnaCijena && i.AkcijskaCijena < request.KrajnjaCijena)).Select(x => new IgriceKategorisiResponseIgrica()
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

            return new IgriceKategorisiResponse
            {
                Igrice = igrice
            };
        }
    }
}
