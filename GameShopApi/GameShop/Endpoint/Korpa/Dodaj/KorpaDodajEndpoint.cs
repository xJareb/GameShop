using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.Korpa.Dodaj
{
    [Tags("Korpa")]
    public class KorpaDodajEndpoint : MyBaseEndpoint<KorpaDodajRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;
        public KorpaDodajEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }
        [HttpPost("DodajUKorpu")]
        public override async Task<NoResponse> Obradi([FromBody]KorpaDodajRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.jelLogiran())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
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
