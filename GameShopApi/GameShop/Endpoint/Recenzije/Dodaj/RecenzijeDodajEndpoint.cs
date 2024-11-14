using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.Recenzije.Dodaj
{
    [Tags("Recenzije")]
    public class RecenzijeDodajEndpoint : MyBaseEndpoint<RecenzijeDodajRequest,NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public RecenzijeDodajEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpPost("DodajRecenziju")]
        public override async Task<NoResponse> Obradi([FromBody]RecenzijeDodajRequest request, CancellationToken cancellationToken = default)
        {
            if (!_myAuthService.jelLogiran())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var provjeraDuplikata = _applicationDbContext.Recenzije.Where(r => request.KorisnikID == r.KorisnikID && request.IgricaID == r.IgricaID).FirstOrDefault();
            if (provjeraDuplikata != null)
                throw new Exception($"{HttpStatusCode.Conflict}");
            var novaRecenzija = new Data.Models.Recenzije()
            {
                Sadrzaj = request.Sadrzaj,
                Ocjena = request.Ocjena,
                KorisnikID = request.KorisnikID,
                IgricaID = request.IgricaID,
            };

            _applicationDbContext.Recenzije.Add(novaRecenzija);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
