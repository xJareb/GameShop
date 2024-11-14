using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GameShop.Endpoint.Kupovina.Pretrazi
{
    [Tags("Kupovina")]
    public class KupovinaPretraziEndpoint : MyBaseEndpoint<NoRequest,KupovinaPretraziResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public KupovinaPretraziEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpGet("IzlistajKupovine")]
        public override async Task<KupovinaPretraziResponse> Obradi([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            if (!_myAuthService.jelLogiran())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }

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
