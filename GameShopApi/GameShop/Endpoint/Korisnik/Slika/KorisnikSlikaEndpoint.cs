using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace GameShop.Endpoint.Korisnik.Slika
{
    [Tags("Korisnik")]
    public class KorisnikSlikaEndpoint : MyBaseEndpoint<KorisnikSlikaRequest,NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KorisnikSlikaEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPut("Slika")]
        public override async Task<NoResponse> Obradi([FromForm]KorisnikSlikaRequest request, CancellationToken cancellationToken = default)
        {
            var logiraniKorisnik = _applicationDbContext.AutentifikacijaToken.FirstOrDefault();
            if (logiraniKorisnik == null)
                throw new Exception("Korisnik nije logiran");
            var logiraniKorisnikID = logiraniKorisnik.KorisnickiNalogID;

            var korisnik = _applicationDbContext.Korisnik.Where(k => k.Id == logiraniKorisnikID).FirstOrDefault();
            if (korisnik == null)
                throw new Exception("Korisnik nije pronađen za id: " + logiraniKorisnikID);

            if(request.Slika != null && request.Slika.Length > 0)
            {
                using(var memoryStream = new MemoryStream())
                {
                    await request.Slika.CopyToAsync(memoryStream);

                    korisnik.Slika = memoryStream.ToArray();

                    _applicationDbContext.Korisnik.Update(korisnik);
                    await _applicationDbContext.SaveChangesAsync();
                }
                return new NoResponse();
            }
            return new NoResponse();
        }
    }
}
