using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.ShoppingCart.Delete
{
    [Tags("ShoppingCart")]
    public class ShoppingCartDeleteEndpoint : MyBaseEndpoint<ShoppingCartDeleteRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ShoppingCartDeleteEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpDelete("CartDelete")]
        public override async Task<NoResponse> Obradi([FromQuery] ShoppingCartDeleteRequest request, CancellationToken cancellationToken = default)
        {
            var game = _applicationDbContext.ShoppingCart.Where(i => i.ID == request.ID).FirstOrDefault();
            if (game == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            _applicationDbContext.ShoppingCart.Remove(game);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
