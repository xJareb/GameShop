using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Recenzije.PregledPoIgrici
{
    [Tags("Recenzije")]
    public class PregledPoIgriciEndpoint : MyBaseEndpoint<PregledPoIgriciRequest,PregledPoIgriciResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PregledPoIgriciEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("PrikaziRecenzije")]
        public override async Task<PregledPoIgriciResponse> Obradi([FromQuery]PregledPoIgriciRequest request, CancellationToken cancellationToken = default)
        {
            var recenzije = await _applicationDbContext.Recenzije.Where(r => request.IgricaID == r.IgricaID || request.IgricaID == 0).Select(x => new PregledPoIgriciResponsePregled()
            {
                Sadrzaj = x.Sadrzaj,
                Ocjena = x.Ocjena,
                Slika = x.Korisnik.Slika,
                KorisnickoIme = x.Korisnik.KNalog.KorisnickoIme,
                Igrica = x.Igrice.Naziv
            }).ToListAsync();

            return new PregledPoIgriciResponse
            {
                Recenzije = recenzije
            };
        }
    }
}
