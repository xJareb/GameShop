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
            if (!_myAuthService.jelLogiran())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }

            var purchases = await _applicationDbContext.Kupovine.Include(i => i.Igrice).Select(x => new PurchasesGetResponsePurchase()
            {
                ID = x.Id,
                PurchaseDate = x.DatumKupovine,
                UserID = x.KorisnikID,
                User = x.Korisnik.KNalog.KorisnickoIme ?? x.Korisnik.Ime,
                Games = x.Igrice.Select(i => new PurchasedGames()
                {
                    ID = i.Id,
                    Name = i.Naziv,
                    Photo = i.Slika,
                    ActionPrice = i.AkcijskaCijena ?? i.Cijena
                }).ToList()
            }).ToListAsync();

            return new PurchasesGetResponse
            {
                Purchases = purchases
            };
        }
    }
}
