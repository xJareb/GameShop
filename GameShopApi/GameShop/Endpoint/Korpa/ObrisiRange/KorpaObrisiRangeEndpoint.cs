using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GameShop.Endpoint.Korpa.ObrisiRange
{
    [Tags("Korpa")]
    public class KorpaObrisiRangeEndpoint : MyBaseEndpoint <KorpaObrisiRangeRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public KorpaObrisiRangeEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpDelete("ObrisiKorpuKorisnika")]
        public override async Task<NoResponse> Obradi([FromQuery]KorpaObrisiRangeRequest request, CancellationToken cancellationToken = default)
        {
            var listaKorpe = await _applicationDbContext.Korpa.Where(k => request.KorisnikID == k.KorisnikID).ToListAsync();

            if(!listaKorpe.Any())
                throw new Exception($"{HttpStatusCode.NotFound}");

            _applicationDbContext.Korpa.RemoveRange(listaKorpe);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
