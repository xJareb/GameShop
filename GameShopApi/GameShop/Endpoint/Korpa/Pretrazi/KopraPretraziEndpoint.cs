using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Korpa.Pretrazi
{
    [Tags("Korpa")]
    public class KopraPretraziEndpoint : MyBaseEndpoint<KorpaPretraziRequest, KorpaPretraziResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KopraPretraziEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("PretraziKorpu")]
        public override async Task<KorpaPretraziResponse> Obradi([FromQuery]KorpaPretraziRequest request, CancellationToken cancellationToken = default)
        {
            var korpa = await _applicationDbContext.Korpa.Where(k => k.Korisnik.Id == request.Id).Select(x => new KorpaPretraziResponseKorpa()
            {
                ID = x.Id,
                Naziv = x.Igrica.Naziv,
                Slika = x.Igrica.Slika,
                PravaCijena = x.Igrica.Cijena,
                AkcijskaCijena = x.Igrica.AkcijskaCijena ?? x.Igrica.Cijena,
                Kolicina = x.Kolicina
                
            }).ToListAsync();

            return new KorpaPretraziResponse()
            {
                Korpa = korpa
            };
        }
    }
}
