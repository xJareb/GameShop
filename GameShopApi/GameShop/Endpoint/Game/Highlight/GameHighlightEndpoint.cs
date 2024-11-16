using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.Game.Highlight
{
    [Tags("Games")]
    public class GameHighlightEndpoint : MyBaseEndpoint<GameHighlightRequest, GameHighLightResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public GameHighlightEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }
        [HttpPut("GameHighlight")]
        public override async Task<GameHighLightResponse> Obradi([FromQuery] GameHighlightRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.isAdmin())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var game = _applicationDbContext.Game.Where(i => i.ID == request.GameID).FirstOrDefault();
            if (game == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            game.Highlighted = request.Highlighted;

            _applicationDbContext.Update(game);
            await _applicationDbContext.SaveChangesAsync();

            return new GameHighLightResponse()
            {
                GameID = request.GameID
            };
        }
    }
}
