using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Korisnik.PregledLog
{
    [Tags("Korisnik")]
    public class KorisnikPregledLogEndpoint : MyBaseEndpoint<KorisnikPregledLogRequest,KorisnikPregledLogResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KorisnikPregledLogEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("PregledLog")]
        public override async Task<KorisnikPregledLogResponse> Obradi([FromQuery]KorisnikPregledLogRequest request, CancellationToken cancellationToken = default)
        {
            var logiraniKorisnik = await _applicationDbContext.Korisnik.Include(k=>k.KNalog).Where(lk => lk.Id == request.LogiraniKorisnikID).Select(x => new KorisnikPregledLogResponseKorisnik()
            {
                ID = x.Id,
                Ime = x.Ime,
                Prezime = x.Prezime,
                //KorisnickiNalog = x.KNalog,
                KorisnickoIme = x.KNalog.KorisnickoIme,
                Email = x.KNalog.Email,
                DatumRodjenja = x.KNalog.DatumRodjenja,
                Slika = x.Slika 
            }).ToListAsync();

            return new KorisnikPregledLogResponse()
            {
                Korisnik = logiraniKorisnik
            };
        }
    }
}
