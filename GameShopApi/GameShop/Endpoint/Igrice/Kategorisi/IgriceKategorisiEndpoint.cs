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
            var upit = _applicationDbContext.Igrice.Where
                (i => (i.ZanrID == request.ZanrID || request.ZanrID == 0) && (i.AkcijskaCijena > request.PocetnaCijena && i.AkcijskaCijena < request.KrajnjaCijena) && (request.NazivIgrice == null || i.Naziv.ToLower().StartsWith(request.NazivIgrice.ToLower())));

            if (request.Sortiranje == "desc")
            {
                upit = upit.OrderByDescending(i => i.Naziv);
            }
            if (request.Sortiranje == "asc")
            {
                upit = upit.OrderBy(i => i.Naziv);
            }
            var igrice = await upit.Select(x => new IgriceKategorisiResponseIgrica()
            {
                Id = x.Id,
                Naziv = x.Naziv,
                ZanrID = x.ZanrID,
                Zanr = x.Zanr.Naziv,
                DatumIzlaska = x.DatumIzlaska,
                Slika = x.Slika,
                Izdavac = x.Izdavac,
                Opis = x.Opis,
                Cijena = x.Cijena,
                PostotakAkcije = x.PostotakAkcije,
                AkcijskaCijena = x.AkcijskaCijena ?? 0
            }).ToListAsync(cancellationToken);


            return new IgriceKategorisiResponse
            {
                Igrice = igrice
            };
        }
    }
}
