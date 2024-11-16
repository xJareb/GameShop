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

        public GameUpdateEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpPut("GameUpdate")]
        public override async Task<GameUpdateResponse> Obradi([FromBody] GameUpdateRequest request, CancellationToken cancellationToken = default)
        {
            if (!_myAuthService.jelAdmin())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var game = _applicationDbContext.Igrice.Where(i => i.Id == request.GameID).FirstOrDefault();
            if (game == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            game.Naziv = request.Name;
            game.ZanrID = request.GenreID;
            game.DatumIzlaska = request.ReleaseDate;
            game.Slika = request.Photo;
            game.Izdavac = request.Publisher;
            game.Opis = request.Description;
            game.Cijena = request.Price;
            game.PostotakAkcije = request.PercentageDiscount;
            game.AkcijskaCijena = request.Price - request.Price * (request.PercentageDiscount / 100);

            _applicationDbContext.Update(game);
            await _applicationDbContext.SaveChangesAsync();

            return new GameUpdateResponse()
            {

            };
        }
    }
}
