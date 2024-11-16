using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.ShoppingCart.Get
{
    [Tags("ShoppingCart")]
    public class ShoppingCartGetEndpoint : MyBaseEndpoint<ShoppingCartGetRequest, ShoppingCartGetResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ShoppingCartGetEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("CartGet")]
        public override async Task<ShoppingCartGetResponse> Obradi([FromQuery] ShoppingCartGetRequest request, CancellationToken cancellationToken = default)
        {
            var cart = await _applicationDbContext.ShoppingCart.Where(k => k.User.ID == request.ID).Select(x => new ShoppingCartGetResponseShoppingCart()
            {
                ID = x.ID,
                Name = x.Game.Name,
                Photo = x.Game.Photo,
                Price = x.Game.Price,
                ActionPrice = x.Game.ActionPrice ?? x.Game.Price,
                Quantity = x.Quantity

            }).ToListAsync();

            return new ShoppingCartGetResponse()
            {
                Cart = cart
            };
        }
    }
}
