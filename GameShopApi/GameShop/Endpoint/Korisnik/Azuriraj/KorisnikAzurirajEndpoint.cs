using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GameShop.Endpoint.Korisnik.Azuriraj
{
    [Tags("Korisnik")]
    public class KorisnikAzurirajEndpoint : MyBaseEndpoint<KorisnikAzurirajRequest,KorisnikAzurirajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KorisnikAzurirajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPut("AzurirajKorisnika")]
        public override async Task<KorisnikAzurirajResponse> Obradi(KorisnikAzurirajRequest request, CancellationToken cancellationToken = default)
        {
            var korisnik = _applicationDbContext.Korisnik.Include(kn => kn.KNalog).Where(k => k.Id == request.KorisnikID).FirstOrDefault();
            if (korisnik == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            korisnik.Ime = request.Ime;
            korisnik.Prezime = request.Prezime;
            korisnik.KNalog.Email = request.Email;

            if(LozinkaHasher.VerifikujLozinku(request.Lozinka,korisnik.KNalog.Lozinka))
            {
                _applicationDbContext.Update(korisnik);
                await _applicationDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Lozinka nije ispravna");
            }

            return new KorisnikAzurirajResponse()
            {
                korisnik = korisnik
            };
        }
    }
}
