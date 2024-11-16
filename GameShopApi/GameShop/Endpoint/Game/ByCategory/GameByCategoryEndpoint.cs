using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Game.ByCategory
{
    [Tags("Games")]
    public class GameByCategoryEndpoint : MyBaseEndpoint<GameByCategoryRequest, GameByCategoryResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GameByCategoryEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("GetByCategory")]
        public override async Task<GameByCategoryResponse> Obradi([FromQuery] GameByCategoryRequest request, CancellationToken cancellationToken = default)
        {
            var upit = _applicationDbContext.Game.Where
                (i => (i.GenreID == request.GenreID || request.GenreID == 0) && i.ActionPrice > request.FirstPrice && i.ActionPrice < request.LastPrice && (request.GameName == null || i.Name.ToLower().StartsWith(request.GameName.ToLower())));

            if (request.Sorting == "desc")
            {
                upit = upit.OrderByDescending(i => i.Name);
            }
            if (request.Sorting == "asc")
            {
                upit = upit.OrderBy(i => i.Name);
            }
            var games = await upit.Select(x => new GameByCategoryResponseGame()
            {
                ID = x.ID,
                Name = x.Name,
                GenreID = x.GenreID,
                Genre = x.Genre.Name,
                ReleaseDate = x.ReleaseDate,
                Photo = x.Photo,
                Publisher = x.Publisher,
                Description = x.Description,
                Price = x.Price,
                PercentageDiscount = x.PercentageDiscount,
                ActionPrice = x.ActionPrice ?? 0
            }).ToListAsync(cancellationToken);


            return new GameByCategoryResponse
            {
                Games = games
            };
        }
    }
}
