using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GameShop.Endpoint.Purchases.Add
{
    [Tags("Purchases")]
    public class PurchasesAddEndpoint : MyBaseEndpoint<PurchasesAddRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly EmailSender _emailSender;
        private readonly MyAuthService _myAuthService;
        public PurchasesAddEndpoint(ApplicationDbContext applicationDbContext, EmailSender emailSender, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _emailSender = emailSender;
            _myAuthService = myAuthService;
        }

        [HttpPost("PurchaseAdd")]
        public override async Task<NoResponse> Obradi([FromBody] PurchasesAddRequest request, CancellationToken cancellationToken = default)
        {
            if (!_myAuthService.isLogged())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var user = _applicationDbContext.User.Include(kn => kn.UserAccount).Where(k => k.ID == request.UserID).FirstOrDefault();

            if (user == null)
                throw new Exception($"{HttpStatusCode.NotFound}");
            var cartList = await _applicationDbContext.ShoppingCart.Include(i => i.Game).Where(k => request.UserID == k.UserID).ToListAsync();
            var emailKorisnik = user.UserAccount.Email;
            if (!cartList.Any())
                throw new Exception($"{HttpStatusCode.NotFound}");
            var games = cartList.Select(k => k.Game).ToList();
            var newPurchase = new Data.Models.Purchases
            {
                BirthDate = DateTime.UtcNow,
                UserID = request.UserID,
                Games = games
            };

            List<Data.Models.Games> UserGames = newPurchase.Games;
            _applicationDbContext.Purchases.Add(newPurchase);
            await _applicationDbContext.SaveChangesAsync();

            _emailSender.SendEmail(emailKorisnik, UserGames);

            return new NoResponse();
        }
    }
}
