using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GameShop.Endpoint.Korisnik.PregledLog
{
    [Tags("Korisnik")]
    public class KorisnikPregledLogEndpoint : MyBaseEndpoint<KorisnikPregledLogRequest,KorisnikPregledLogResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public KorisnikPregledLogEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }
        [HttpGet("PregledLog")]
        public override async Task<KorisnikPregledLogResponse> Obradi([FromQuery]KorisnikPregledLogRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.jelLogiran())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var brojNarudzbi = _applicationDbContext.Kupovine.Where(k => k.KorisnikID == request.LogiraniKorisnikID).Count();
            var logiraniKorisnik = await _applicationDbContext.Korisnik.Include(k=>k.KNalog).Where(lk => lk.Id == request.LogiraniKorisnikID).Select(x => new KorisnikPregledLogResponseKorisnik()
            {
                ID = x.Id,
                Ime = x.Ime,
                Prezime = x.Prezime,
                //KorisnickiNalog = x.KNalog,
                KorisnickoIme = x.KNalog.KorisnickoIme,
                Email = x.KNalog.Email,
                DatumRodjenja = x.KNalog.DatumRodjenja,
                Slika = x.Slika,
                BrojNarudzbi = brojNarudzbi,
                GoogleSlika = x.GoogleSlika
            }).ToListAsync();
            return new KorisnikPregledLogResponse()
            {
                Korisnik = logiraniKorisnik
            };
        }
    }
}
