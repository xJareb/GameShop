using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.ShoppingCart.Update
{
    [Tags("ShoppingCart")]
    public class ShoppingCartUpdateEndpoint : MyBaseEndpoint<ShoppingCartUpdateRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;
        private readonly TokenValidation _tokenValidation;

        public ShoppingCartUpdateEndpoint(ApplicationDbContext applicationDbContext, TokenValidation tokenValidation, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _tokenValidation = tokenValidation;
            _authService = authService;
        }
        [HttpPut("CartUpdate")]
        public override async Task<NoResponse> Obradi([FromBody] ShoppingCartUpdateRequest request, CancellationToken cancellationToken = default)
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

            game.Quantity = request.Quantity;
            _applicationDbContext.ShoppingCart.Update(game);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
