using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Game.Offer
{
    [Tags("Games")]
    public class GameOfferEndpoint : MyBaseEndpoint<NoRequest, GameOfferResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GameOfferEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("GameOffer")]
        public override async Task<GameOfferResponse> Obradi([FromQuery] NoRequest request, CancellationToken cancellationToken = default)
        {
            var offer = await _applicationDbContext.Igrice.Where(i => i.Izdvojeno == true).Select(x => new GameOfferResponseGame()
            {
                ID = x.Id,
                Name = x.Naziv,
                Photo = x.Slika,
                ActionPrice = x.AkcijskaCijena,
                PercentageDiscount = x.PostotakAkcije
            }).ToListAsync();

            return new GameOfferResponse()
            {
                Games = offer
            };
        }
    }
}
