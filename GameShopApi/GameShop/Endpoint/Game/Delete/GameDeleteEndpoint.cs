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
        private readonly TokenValidation _tokenValidation;

        public GameDeleteEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService, TokenValidation tokenValidation)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
            _tokenValidation = tokenValidation;
        }

        [HttpDelete("GameDelete")]
        public override async Task<NoResponse> Obradi([FromQuery] GameDeleteRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.isAdmin())
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

            _applicationDbContext.Remove(game);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
