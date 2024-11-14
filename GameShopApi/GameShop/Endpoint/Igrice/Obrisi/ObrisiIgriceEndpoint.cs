using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.Igrice.Obrisi
{
    [Tags("Igrice")]
    public class ObrisiIgriceEndpoint : MyBaseEndpoint<ObrisiIgriceRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public ObrisiIgriceEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }

        [HttpDelete("ObrisiIgricu")]
        public override async Task<NoResponse> Obradi([FromQuery]ObrisiIgriceRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.jelAdmin())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var igrica = _applicationDbContext.Igrice.Where(i => i.Id == request.IgricaID).FirstOrDefault();
            if(igrica == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            _applicationDbContext.Remove(igrica);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
