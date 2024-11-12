using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.Korisnik.Dodaj
{
    [Tags("Korisnik")]
    [Route("Dodaj")]
    public class KorisnikDodajEndpoint : MyBaseEndpoint<KorisnikDodajRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public KorisnikDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost]
        public override async Task<NoResponse> Obradi([FromBody]KorisnikDodajRequest request, CancellationToken cancellationToken = default)
        {
            var korisnickiNalog = new KorisnickiNalog()
            {
                KorisnickoIme = request.KorisnickoIme,
                Email = request.Email,
                Lozinka = LozinkaHasher.HashPassword(request.Lozinka),
                DatumRodjenja = request.DatumRodjenja,
                isAdmin = false,
                isKorisnik = true
            };

            var provjeraKorisnickogImena = _applicationDbContext.KorisnickiNalog.Where(kn => kn.KorisnickoIme == request.KorisnickoIme).FirstOrDefault();
            if (provjeraKorisnickogImena != null)
                throw new Exception($"{HttpStatusCode.Conflict}");

            var provjeraEmaila = _applicationDbContext.KorisnickiNalog.Where(e=>e.Email == request.Email).FirstOrDefault();
            if (provjeraEmaila != null)
                throw new Exception($"{HttpStatusCode.Conflict}");

            _applicationDbContext.KorisnickiNalog.Add(korisnickiNalog);
            await _applicationDbContext.SaveChangesAsync();

            var korisnik = new Data.Models.Korisnik()
            {
                Ime = request.Ime,
                Prezime = request.Prezime,
                KorisnickiNalogID = korisnickiNalog.Id
            };
            _applicationDbContext.Korisnik.Add(korisnik);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse
            {

            };
        }
    }
}
