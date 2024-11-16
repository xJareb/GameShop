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
            var cart = await _applicationDbContext.Korpa.Where(k => k.Korisnik.Id == request.ID).Select(x => new ShoppingCartGetResponseShoppingCart()
            {
                ID = x.Id,
                Name = x.Igrica.Naziv,
                Photo = x.Igrica.Slika,
                Price = x.Igrica.Cijena,
                ActionPrice = x.Igrica.AkcijskaCijena ?? x.Igrica.Cijena,
                Quantity = x.Kolicina

            }).ToListAsync();

            return new ShoppingCartGetResponse()
            {
                Cart = cart
            };
        }
    }
}
