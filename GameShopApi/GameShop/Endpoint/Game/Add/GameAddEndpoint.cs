using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Endpoint.Game.Add;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.Igrice.Dodaj
{
    [Tags("Games")]
    public class GameAddEndpoint : MyBaseEndpoint<GameAddRequest, GameAddResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public GameAddEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }

        [HttpPost("GameAdd")]
        public override async Task<GameAddResponse> Obradi([FromBody]GameAddRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.jelAdmin())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var novaIgrica = new Data.Models.Igrice()
            {
                Naziv = request.Name,
                ZanrID = request.GenreID,
                DatumIzlaska = request.ReleaseDate,
                Slika = request.Photo,
                Izdavac = request.Publisher,
                Opis = request.Description,
                Cijena = request.Price,
                PostotakAkcije = request.PercentageDiscount,
                AkcijskaCijena = request.Price - (request.Price * (request.PercentageDiscount / 100))
            };
            _applicationDbContext.Add(novaIgrica);
            await _applicationDbContext.SaveChangesAsync();

            return new GameAddResponse()
            {

            };
        }
    }
}
