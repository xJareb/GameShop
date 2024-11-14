using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GameShop.Endpoint.Korpa.ObrisiRange
{
    [Tags("Korpa")]
    public class KorpaObrisiRangeEndpoint : MyBaseEndpoint <KorpaObrisiRangeRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;
        public KorpaObrisiRangeEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpDelete("ObrisiKorpuKorisnika")]
        public override async Task<NoResponse> Obradi([FromQuery]KorpaObrisiRangeRequest request, CancellationToken cancellationToken = default)
        {
            if (!_myAuthService.jelLogiran())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var listaKorpe = await _applicationDbContext.Korpa.Where(k => request.KorisnikID == k.KorisnikID).ToListAsync();

            if(!listaKorpe.Any())
                throw new Exception($"{HttpStatusCode.NotFound}");

            _applicationDbContext.Korpa.RemoveRange(listaKorpe);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
