using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Endpoint.Korisnik.DodajGoogle
{
    [Tags("Korisnik")]
    public class KorisnikDodajGoogleEndpoint : MyBaseEndpoint<KorisnikDodajGoogleRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KorisnikDodajGoogleEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost("DodajGoogleKorisnik")]
        public override async Task<NoResponse> Obradi([FromBody]KorisnikDodajGoogleRequest request, CancellationToken cancellationToken = default)
        {
            var provjeraEmaila = _applicationDbContext.KorisnickiNalog.Where(kn => kn.Email == request.Email).FirstOrDefault();
            var googleKorisnik = new KorisnickiNalog()
            {
                Email = request.Email,
                isGoogleProvider = request.isGoogleProvider,
                isKorisnik = true
            };

            if(provjeraEmaila == null)
            {
                _applicationDbContext.KorisnickiNalog.Add(googleKorisnik);
                await _applicationDbContext.SaveChangesAsync();
            }
            var korisnik = new Data.Models.Korisnik()
            {
                Ime = request.Ime,
                GoogleSlika = request.Slika,
                KorisnickiNalogID = googleKorisnik.Id
            };

            if (provjeraEmaila == null)
            {
                _applicationDbContext.Korisnik.Add(korisnik);
                await _applicationDbContext.SaveChangesAsync();
            }
            return new NoResponse();
        }
    }
}
