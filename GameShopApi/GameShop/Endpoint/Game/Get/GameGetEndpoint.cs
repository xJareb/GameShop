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
            var games = await _applicationDbContext.Igrice.Where(i => request.GameID == i.Id || request.GameID == 0).Select(x => new GameGetResponseGame()
            {
                ID = x.Id,
                Name = x.Naziv,
                Genre = x.Zanr.Naziv,
                ReleaseDate = x.DatumIzlaska,
                Photo = x.Slika,
                Publisher = x.Izdavac,
                Description = x.Opis,
                Price = x.Cijena,
                PercentageDiscount = x.PostotakAkcije,
                ActionPrice = x.AkcijskaCijena ?? 0
            }).ToListAsync();

            return new GameGetResponse
            {
                Game = games
            };
        }
    }
}
