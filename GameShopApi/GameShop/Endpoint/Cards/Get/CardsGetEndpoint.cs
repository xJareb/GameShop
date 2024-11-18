using System.Net;
using System.Text;
using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Cards.Get
{
    [Tags("Cards")]
    public class CardsGetEndpoint : MyBaseEndpoint<CardsGetRequest, CardsGetResponse>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MyAuthService _authService;

        public CardsGetEndpoint(ApplicationDbContext dbContext,MyAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }
        [HttpGet("CardGetBy")]
        public override async Task<CardsGetResponse> Obradi([FromQuery]CardsGetRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.isLogged())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            byte[] key = Encoding.UTF8.GetBytes("1234567890123456");
            byte[] iv = Encoding.UTF8.GetBytes("1234567890123456");

            var cardData = await _dbContext.Card.Where(cd => cd.UserID == request.UserID).Select(x =>
                new CardsGetResponseCard()
                {
                    CardNumber = (EncryptDecryptString.DecryptString(x.CardNumber, key, iv)).Substring(EncryptDecryptString.DecryptString(x.CardNumber, key, iv).Length - 4)
                }).ToListAsync();

            return new CardsGetResponse()
            {
                Cards = cardData
            };
        }
    }
}
