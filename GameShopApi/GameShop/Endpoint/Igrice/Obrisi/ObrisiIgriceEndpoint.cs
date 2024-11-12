using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.Igrice.Obrisi
{
    [Tags("Igrice")]
    public class ObrisiIgriceEndpoint : MyBaseEndpoint<ObrisiIgriceRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ObrisiIgriceEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete("ObrisiIgricu")]
        public override async Task<NoResponse> Obradi([FromQuery]ObrisiIgriceRequest request, CancellationToken cancellationToken = default)
        {
            var igrica = _applicationDbContext.Igrice.Where(i => i.Id == request.IgricaID).FirstOrDefault();
            if(igrica == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            _applicationDbContext.Remove(igrica);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
