using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GameShop.Endpoint.Purchases.Get
{
    [Tags("Purchases")]
    public class PurchasesGetEndpoint : MyBaseEndpoint<NoRequest, PurchasesGetResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public PurchasesGetEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpGet("PurchaseGet")]
        public override async Task<PurchasesGetResponse> Obradi([FromQuery] NoRequest request, CancellationToken cancellationToken = default)
        {
            if (!_myAuthService.isLogged())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }

            var purchases = await _applicationDbContext.Purchases.Include(i => i.Games).Select(x => new PurchasesGetResponsePurchase()
            {
                ID = x.ID,
                PurchaseDate = x.PurchaseDate,
                UserID = x.UserID,
                User = x.User.UserAccount.Username ?? x.User.Name,
                Games = x.Games.Select(i => new PurchasedGames()
                {
                    ID = i.ID,
                    Name = i.Name,
                    Photo = i.Photo,
                    ActionPrice = i.ActionPrice ?? i.Price
                }).ToList()
            }).ToListAsync();

            return new PurchasesGetResponse
            {
                Purchases = purchases
            };
        }
    }
}
