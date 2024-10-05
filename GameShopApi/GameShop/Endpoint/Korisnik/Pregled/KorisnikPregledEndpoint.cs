using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Korisnik.Pregled
{
    [Tags("Korisnik")]
    public class KorisnikPregledEndpoint : MyBaseEndpoint<NoRequest, KorisnikPregledResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KorisnikPregledEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("PregledSvih")]
        public override async Task<KorisnikPregledResponse> Obradi([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            var korisnici = await _applicationDbContext.Korisnik.Include(kn => kn.KNalog).Where(k => k.KNalog.isKorisnik == true).Select(x => new KorisnikPregledResponseKorisnik()
            {
                ID = x.Id,
                Ime = x.Ime,
                Prezime = x.Prezime,
                KorisnickoIme = x.KNalog.KorisnickoIme,
                Email = x.KNalog.Email
            }).ToListAsync();


            return new KorisnikPregledResponse()
            {
                Korisnici = korisnici
            };
        }
    }
}
