using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Endpoint.Korisnik.Dodaj
{
    [Tags("Korisnik")]
    [Route("Dodaj")]
    public class KorisnikDodajResponse : MyBaseEndpoint<KorisnikDodajRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public KorisnikDodajResponse(ApplicationDbContext applicationDbContext)
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
                Lozinka = request.Lozinka,
                isAdmin = false,
                isKorisnik = true
            };

            var provjeraKorisnickogImena = _applicationDbContext.KorisnickiNalog.Where(kn => kn.KorisnickoIme == request.KorisnickoIme).FirstOrDefault();
            if (provjeraKorisnickogImena != null)
                throw new Exception("Korisničko ime već postoji");

            var provjeraEmaila = _applicationDbContext.KorisnickiNalog.Where(e=>e.Email == request.Email).FirstOrDefault();
            if (provjeraEmaila != null)
                throw new Exception("Email već postoji");

            _applicationDbContext.KorisnickiNalog.Add(korisnickiNalog);
            await _applicationDbContext.SaveChangesAsync();

            var korisnik = new Data.Models.Korisnik()
            {
                Ime = request.KorisnickoIme,
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
