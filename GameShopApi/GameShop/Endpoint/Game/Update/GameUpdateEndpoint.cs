using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.Game.Update
{
    [Tags("Games")]
    public class GameUpdateEndpoint : MyBaseEndpoint<GameUpdateRequest, GameUpdateResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;
        private readonly TokenValidation _tokenValidation;

        public GameUpdateEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService, TokenValidation tokenValidation)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
            _tokenValidation = tokenValidation;
        }
        [HttpPut("GameUpdate")]
        public override async Task<GameUpdateResponse> Obradi([FromBody] GameUpdateRequest request, CancellationToken cancellationToken = default)
        {
            if (!_myAuthService.isAdmin())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            if (!_tokenValidation.checkTokenValidation())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized} : Token expired");
            }
            var game = _applicationDbContext.Game.Where(i => i.ID == request.GameID).FirstOrDefault();
            if (game == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            game.Name = request.Name;
            game.GenreID = request.GenreID;
            game.ReleaseDate = request.ReleaseDate;
            game.Photo = request.Photo;
            game.Publisher = request.Publisher;
            game.Description = request.Description;
            game.Price = request.Price;
            game.PercentageDiscount = request.PercentageDiscount;
            game.ActionPrice = request.Price - request.Price * (request.PercentageDiscount / 100);

            _applicationDbContext.Update(game);
            await _applicationDbContext.SaveChangesAsync();

            return new GameUpdateResponse()
            {

            };
        }
    }
}
