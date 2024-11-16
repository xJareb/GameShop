using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace GameShop.Endpoint.Cards.Add
{
    [Tags("Cards")]
    public class CardsAddEndpoint : MyBaseEndpoint<CarsAddRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public CardsAddEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }
        [HttpPost("CardAdd")]
        public override async Task<NoResponse> Obradi(CarsAddRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.jelLogiran())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            byte[] key = Encoding.UTF8.GetBytes("1234567890123456");
            byte[] iv = Encoding.UTF8.GetBytes("1234567890123456");

            var newCard = new Data.Models.Kartica()
            {
                BrojKartice = EncryptDecryptString.EncryptString(request.CardNumber, key, iv),
                Istek = DatumHasher.HashDate(request.ExpirationDate),
                KorisnikID = request.UserID
            };
            _applicationDbContext.Kartica.Add(newCard);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
