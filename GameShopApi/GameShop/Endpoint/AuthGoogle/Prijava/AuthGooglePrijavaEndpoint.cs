using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.AuthGoogle.Prijava
{
    [Tags("GoogleAuth")]
    public class AuthGooglePrijavaEndpoint : MyBaseEndpoint<AuthGooglePrijavaReuqest,MyAuthInfo>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AuthGooglePrijavaEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost("Prijavi-se-google")]
        public override async Task<MyAuthInfo> Obradi([FromQuery]AuthGooglePrijavaReuqest request, CancellationToken cancellationToken = default)
        {
            var logiraniKorisnik = await _applicationDbContext.Korisnik.Include(kn => kn.KNalog)
                .FirstOrDefaultAsync(k =>
                    k.KNalog.Email == request.Email && k.KNalog.isGoogleProvider == true);

            if (logiraniKorisnik == null)
            {
                return new MyAuthInfo(null);
            }
            string randomString = TokenGenerator.Generate(10);

            var noviToken = new AutentifikacijaToken()
            {
                ipAdresa = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                vrijednost = randomString,
                korisnickiNalog = logiraniKorisnik.KNalog,
                vrijemeEvidentiranja = DateTime.Now,
                KorisnikID = logiraniKorisnik.Id
            };
            _applicationDbContext.Add(noviToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new MyAuthInfo(noviToken);
        }
    }
}
