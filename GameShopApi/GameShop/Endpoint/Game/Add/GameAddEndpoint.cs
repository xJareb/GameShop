﻿using GameShop.Data;
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
            if (!_authService.isAdmin())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var novaIgrica = new Data.Models.Games()
            {
                Name = request.Name,
                GenreID = request.GenreID,
                ReleaseDate = request.ReleaseDate,
                Photo = request.Photo,
                Publisher = request.Publisher,
                Description = request.Description,
                Price = request.Price,
                PercentageDiscount = request.PercentageDiscount,
                ActionPrice = request.Price - (request.Price * (request.PercentageDiscount / 100))
            };
            _applicationDbContext.Add(novaIgrica);
            await _applicationDbContext.SaveChangesAsync();

            return new GameAddResponse()
            {

            };
        }
    }
}
