using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GameShop.Endpoint.Korisnik.Obrisi
{
    [Tags("Korisnik")]
    public class KorisnikObrisiEndpoint : MyBaseEndpoint<KorisnikObrisiRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public KorisnikObrisiEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }
        [HttpPut("ObrisiKorisnika")]
        public override async Task<NoResponse> Obradi([FromQuery]KorisnikObrisiRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.jelAdmin())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var korisnik = _applicationDbContext.Korisnik.Include(kn=>kn.KNalog).Where(k=>k.Id == request.ID).FirstOrDefault();
            if(korisnik == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            if(request.isBlackList == false){
                korisnik.KNalog.isDeleted = true;
            }
            else{
                korisnik.KNalog.isBlackList = true;
            }
            

            _applicationDbContext.Update(korisnik);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
