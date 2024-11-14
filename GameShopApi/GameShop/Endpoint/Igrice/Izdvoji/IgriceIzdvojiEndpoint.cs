using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.Igrice.Izdvoji
{
    [Tags("Igrice")]
    public class IgriceIzdvojiEndpoint : MyBaseEndpoint<IgriceIzdvojiRequest, IgriceIzdvojiResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public IgriceIzdvojiEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }
        [HttpPut("IzdvojiIgricu")]
        public override async Task<IgriceIzdvojiResponse> Obradi([FromQuery]IgriceIzdvojiRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.jelAdmin())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var igrica = _applicationDbContext.Igrice.Where(i=>i.Id == request.IgricaID).FirstOrDefault();
            if(igrica == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            igrica.Izdvojeno = request.Izdvojeno;

            _applicationDbContext.Update(igrica);
            await _applicationDbContext.SaveChangesAsync();

            return new IgriceIzdvojiResponse()
            {
                IgricaID = request.IgricaID
            };
        }
    }
}
