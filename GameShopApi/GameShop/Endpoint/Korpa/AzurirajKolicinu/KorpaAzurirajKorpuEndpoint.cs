using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.Korpa.AzurirajKolicinu
{
    [Tags("Korpa")]
    public class KorpaAzurirajKorpuEndpoint : MyBaseEndpoint<KorpaAzurirajKorpuRequest,NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KorpaAzurirajKorpuEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPut("AzurirajKolicinu")]
        public override async Task<NoResponse> Obradi([FromBody]KorpaAzurirajKorpuRequest request, CancellationToken cancellationToken = default)
        {
            var igrica = _applicationDbContext.Korpa.Where(i=>i.Id == request.Id).FirstOrDefault();
            if(igrica == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            igrica.Kolicina = request.Kolicina;
            _applicationDbContext.Korpa.Update(igrica);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
