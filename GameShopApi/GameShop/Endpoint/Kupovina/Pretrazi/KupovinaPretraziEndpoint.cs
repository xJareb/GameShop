using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Kupovina.Pretrazi
{
    [Tags("Kupovina")]
    public class KupovinaPretraziEndpoint : MyBaseEndpoint<NoRequest,KupovinaPretraziResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KupovinaPretraziEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("IzlistajKupovine")]
        public override async Task<KupovinaPretraziResponse> Obradi([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {

            var kupovine = await _applicationDbContext.Kupovine.Include(i=>i.Igrice).Select(x=> new KupovinaPretraziResponseKupovina()
            {
                ID = x.Id,
                DatumKupovine = x.DatumKupovine,
                KorisnikID = x.KorisnikID,
                Korisnik = x.Korisnik.KNalog.KorisnickoIme ?? x.Korisnik.Ime,
                Igrice = x.Igrice.Select(i=>new KupljeneIgrice
                {
                    ID = i.Id,
                    Naziv = i.Naziv,
                    Slika = i.Slika,
                    AkcijskaCijena = i.Cijena
                }).ToList()
            }).ToListAsync();

            return new KupovinaPretraziResponse
            {
                Kupovine = kupovine
            };
        }
    }
}
