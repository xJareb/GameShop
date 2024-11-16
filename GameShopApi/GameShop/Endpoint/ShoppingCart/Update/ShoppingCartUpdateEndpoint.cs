using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.ShoppingCart.Update
{
    [Tags("ShoppingCart")]
    public class ShoppingCartUpdateEndpoint : MyBaseEndpoint<ShoppingCartUpdateRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ShoppingCartUpdateEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPut("CartUpdate")]
        public override async Task<NoResponse> Obradi([FromBody] ShoppingCartUpdateRequest request, CancellationToken cancellationToken = default)
        {
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
