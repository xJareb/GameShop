using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Kupovina.Dodaj
{
    [Tags("Kupovina")]
    public class KupovinaDodajEndpoint : MyBaseEndpoint<KupovinaDodajRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public KupovinaDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost("DodajKupovinu")]
        public override async Task<NoResponse> Obradi([FromBody]KupovinaDodajRequest request, CancellationToken cancellationToken = default)
        {
            var korisnik = _applicationDbContext.Korisnik.Where(k=>k.Id == request.KorisnikID).FirstOrDefault();
            if (korisnik == null)
                throw new Exception("Korisnik nije pronađen");
            var korpaLista = await _applicationDbContext.Korpa.Include(i=>i.Igrica).Where(k=>request.KorisnikID == k.KorisnikID).ToListAsync();
            if (!korpaLista.Any())
                throw new Exception("Igrice ne postoje u korpi");
            var igrice = korpaLista.Select(k=>k.Igrica).ToList();
            var novaKupovina = new Data.Models.Kupovine
            {
                DatumKupovine = DateTime.UtcNow,
                KorisnikID = request.KorisnikID,
                Igrice = igrice
            };

            _applicationDbContext.Kupovine.Add(novaKupovina);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
