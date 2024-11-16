using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GameShop.Endpoint.ShoppingCart.DeleteRange
{
    [Tags("ShoppingCart")]
    public class CartDeleteRangeEndpoint : MyBaseEndpoint<CartDeleteRangeRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;
        public CartDeleteRangeEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpDelete("CartDeleteRange")]
        public override async Task<NoResponse> Obradi([FromQuery] CartDeleteRangeRequest request, CancellationToken cancellationToken = default)
        {
            if (!_myAuthService.isLogged())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var cartList = await _applicationDbContext.ShoppingCart.Where(k => request.UserID == k.UserID).ToListAsync();

            if (!cartList.Any())
                throw new Exception($"{HttpStatusCode.NotFound}");

            _applicationDbContext.ShoppingCart.RemoveRange(cartList);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
