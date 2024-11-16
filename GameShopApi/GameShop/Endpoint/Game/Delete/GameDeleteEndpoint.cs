using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.Game.Delete
{
    [Tags("Games")]
    public class GameDeleteEndpoint : MyBaseEndpoint<GameDeleteRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public GameDeleteEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }

        [HttpDelete("GameDelete")]
        public override async Task<NoResponse> Obradi([FromQuery] GameDeleteRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.jelAdmin())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var game = _applicationDbContext.Igrice.Where(i => i.Id == request.GameID).FirstOrDefault();
            if (game == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            _applicationDbContext.Remove(game);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
