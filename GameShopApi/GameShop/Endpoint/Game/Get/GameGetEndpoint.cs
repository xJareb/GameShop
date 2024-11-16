using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Game.Get
{
    [Tags("Games")]
    public class GameGetEndpoint : MyBaseEndpoint<GameGetRequest, GameGetResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public GameGetEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("GamesGet")]
        public override async Task<GameGetResponse> Obradi([FromQuery] GameGetRequest request, CancellationToken cancellationToken = default)
        {
            var games = await _applicationDbContext.Game.Where(i => request.GameID == i.ID || request.GameID == 0).Select(x => new GameGetResponseGame()
            {
                ID = x.ID,
                Name = x.Name,
                Genre = x.Genre.Name,
                ReleaseDate = x.ReleaseDate,
                Photo = x.Photo,
                Publisher = x.Publisher,
                Description = x.Description,
                Price = x.Price,
                PercentageDiscount = x.PercentageDiscount,
                ActionPrice = x.ActionPrice ?? 0
            }).ToListAsync();

            return new GameGetResponse
            {
                Game = games
            };
        }
    }
}
