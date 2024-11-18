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
        private readonly TokenValidation _tokenValidation;

        public CardsAddEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService, TokenValidation tokenValidation)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
            _tokenValidation = tokenValidation;
        }
        [HttpPost("CardAdd")]
        public override async Task<NoResponse> Obradi(CarsAddRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.isLogged())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            if (!_tokenValidation.checkTokenValidation())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized} : Token expired");
            }
            byte[] key = Encoding.UTF8.GetBytes("1234567890123456");
            byte[] iv = Encoding.UTF8.GetBytes("1234567890123456");

            var newCard = new Data.Models.Card()
            {
                CardNumber = EncryptDecryptString.EncryptString(request.CardNumber, key, iv),
                Expiration = DatumHasher.HashDate(request.ExpirationDate),
                UserID = request.UserID
            };
            _applicationDbContext.Card.Add(newCard);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
