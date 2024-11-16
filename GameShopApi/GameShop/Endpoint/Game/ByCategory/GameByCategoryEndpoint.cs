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
            var upit = _applicationDbContext.Igrice.Where
                (i => (i.ZanrID == request.GenreID || request.GenreID == 0) && i.AkcijskaCijena > request.FirstPrice && i.AkcijskaCijena < request.LastPrice && (request.GameName == null || i.Naziv.ToLower().StartsWith(request.GameName.ToLower())));

            if (request.Sorting == "desc")
            {
                upit = upit.OrderByDescending(i => i.Naziv);
            }
            if (request.Sorting == "asc")
            {
                upit = upit.OrderBy(i => i.Naziv);
            }
            var games = await upit.Select(x => new GameByCategoryResponseGame()
            {
                ID = x.Id,
                Name = x.Naziv,
                GenreID = x.ZanrID,
                Genre = x.Zanr.Naziv,
                ReleaseDate = x.DatumIzlaska,
                Photo = x.Slika,
                Publisher = x.Izdavac,
                Description = x.Opis,
                Price = x.Cijena,
                PercentageDiscount = x.PostotakAkcije,
                ActionPrice = x.AkcijskaCijena ?? 0
            }).ToListAsync(cancellationToken);


            return new GameByCategoryResponse
            {
                Games = games
            };
        }
    }
}
