using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.ShoppingCart.Add
{
    [Tags("ShoppingCart")]
    public class ShoppingCartAddEndpoint : MyBaseEndpoint<ShoppingCartAddRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;
        public ShoppingCartAddEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }
        [HttpPost("ShoppingCartAdd")]
        public override async Task<NoResponse> Obradi([FromBody] ShoppingCartAddRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.isLogged())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var checkDuplicates = _applicationDbContext.ShoppingCart.Where(pd => pd.UserID == request.UserID && pd.GameID == request.UserID).FirstOrDefault();
            if (checkDuplicates == null)
            {
                var novaIgrica = new Data.Models.ShoppingCart()
                {
                    UserID = request.UserID,
                    GameID = request.GameID,
                    Quantity = request.Quantity
                };
                if (novaIgrica.Quantity == 0)
                {
                    throw new Exception($"{HttpStatusCode.BadRequest}");
                }
                _applicationDbContext.ShoppingCart.Add(novaIgrica);

            }
            else
            {
                checkDuplicates.Quantity = checkDuplicates.Quantity + request.Quantity;
                _applicationDbContext.ShoppingCart.Update(checkDuplicates);
            }
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
