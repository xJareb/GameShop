using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.Korpa.Dodaj
{
    [Tags("Korpa")]
    public class KorpaDodajEndpoint : MyBaseEndpoint<KorpaDodajRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public KorpaDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost("DodajUKorpu")]
        public override async Task<NoResponse> Obradi([FromBody]KorpaDodajRequest request, CancellationToken cancellationToken = default)
        {
            var provjeraDuplikata = _applicationDbContext.Korpa.Where(pd => pd.KorisnikID == request.KorisnikID && pd.IgricaID == request.IgricaID).FirstOrDefault();
            if (provjeraDuplikata == null)
            {
                var novaIgrica = new Data.Models.Korpa()
                {
                    KorisnikID = request.KorisnikID,
                    IgricaID = request.IgricaID,
                    Kolicina = request.Kolicina
                };
                if(novaIgrica.Kolicina == 0)
                {
                    throw new Exception($"{HttpStatusCode.BadRequest}");
                }
                _applicationDbContext.Korpa.Add(novaIgrica);
                
            }
            else
            {
                provjeraDuplikata.Kolicina = provjeraDuplikata.Kolicina + request.Kolicina;
                _applicationDbContext.Korpa.Update(provjeraDuplikata);
            }
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
