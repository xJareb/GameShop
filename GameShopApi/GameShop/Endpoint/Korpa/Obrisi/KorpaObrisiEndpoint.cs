using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.Korpa.Obrisi
{
    [Tags("Korpa")]
    public class KorpaObrisiEndpoint : MyBaseEndpoint<KorpaObrisiRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public KorpaObrisiEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpDelete("KorpaObrisiIgircu")]
        public override async Task<NoResponse> Obradi([FromQuery]KorpaObrisiRequest request, CancellationToken cancellationToken = default)
        {
            var igrica = _applicationDbContext.Korpa.Where(i=>i.Id == request.ID).FirstOrDefault();
            if(igrica == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            _applicationDbContext.Korpa.Remove(igrica);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
