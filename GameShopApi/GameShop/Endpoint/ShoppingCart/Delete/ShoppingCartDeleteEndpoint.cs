using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using GameShop.Helper.Services;

namespace GameShop.Endpoint.ShoppingCart.Delete
{
    [Tags("ShoppingCart")]
    public class ShoppingCartDeleteEndpoint : MyBaseEndpoint<ShoppingCartDeleteRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;
        private readonly TokenValidation _tokenValidation;
        public ShoppingCartDeleteEndpoint(ApplicationDbContext applicationDbContext,TokenValidation tokenValidation, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _tokenValidation = tokenValidation;
            _authService = authService;
        }
        [HttpDelete("CartDelete")]
        public override async Task<NoResponse> Obradi([FromQuery] ShoppingCartDeleteRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.isLogged())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            if (!_tokenValidation.checkTokenValidation())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized} : Token expired");
            }
            var game = _applicationDbContext.ShoppingCart.Where(i => i.ID == request.ID).FirstOrDefault();
            if (game == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            _applicationDbContext.ShoppingCart.Remove(game);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
