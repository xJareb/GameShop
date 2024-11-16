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
            if (!_myAuthService.jelLogiran())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var user = _applicationDbContext.Korisnik.Include(kn => kn.KNalog).Where(k => k.Id == request.UserID).FirstOrDefault();

            if (user == null)
                throw new Exception($"{HttpStatusCode.NotFound}");
            var cartList = await _applicationDbContext.Korpa.Include(i => i.Igrica).Where(k => request.UserID == k.KorisnikID).ToListAsync();
            var emailKorisnik = user.KNalog.Email;
            if (!cartList.Any())
                throw new Exception($"{HttpStatusCode.NotFound}");
            var games = cartList.Select(k => k.Igrica).ToList();
            var newPurchase = new Data.Models.Kupovine
            {
                DatumKupovine = DateTime.UtcNow,
                KorisnikID = request.UserID,
                Igrice = games
            };

            List<Data.Models.Igrice> UserGames = newPurchase.Igrice;
            _applicationDbContext.Kupovine.Add(newPurchase);
            await _applicationDbContext.SaveChangesAsync();

            _emailSender.Posalji(emailKorisnik, UserGames);

            return new NoResponse();
        }
    }
}
