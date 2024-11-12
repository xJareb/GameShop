using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GameShop.Endpoint.Kupovina.Dodaj
{
    [Tags("Kupovina")]
    public class KupovinaDodajEndpoint : MyBaseEndpoint<KupovinaDodajRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly EmailSender _emailSender;
        public KupovinaDodajEndpoint(ApplicationDbContext applicationDbContext, EmailSender emailSender)
        {
            _applicationDbContext = applicationDbContext;
            _emailSender = emailSender;
        }

        [HttpPost("DodajKupovinu")]
        public override async Task<NoResponse> Obradi([FromBody]KupovinaDodajRequest request, CancellationToken cancellationToken = default)
        {
            var korisnik = _applicationDbContext.Korisnik.Include(kn=>kn.KNalog).Where(k=>k.Id == request.KorisnikID).FirstOrDefault();
            
            if (korisnik == null)
                throw new Exception($"{HttpStatusCode.NotFound}");
            var korpaLista = await _applicationDbContext.Korpa.Include(i=>i.Igrica).Where(k=>request.KorisnikID == k.KorisnikID).ToListAsync();
            var emailKorisnik = korisnik.KNalog.Email;
            if (!korpaLista.Any())
                throw new Exception($"{HttpStatusCode.NotFound}");
            var igrice = korpaLista.Select(k=>k.Igrica).ToList();
            var novaKupovina = new Data.Models.Kupovine
            {
                DatumKupovine = DateTime.UtcNow,
                KorisnikID = request.KorisnikID,
                Igrice = igrice
            };

            List<Data.Models.Igrice> igriceKorisnika = novaKupovina.Igrice;
            _applicationDbContext.Kupovine.Add(novaKupovina);
            await _applicationDbContext.SaveChangesAsync();

            _emailSender.Posalji(emailKorisnik,igriceKorisnika);

            return new NoResponse();
        }
    }
}
