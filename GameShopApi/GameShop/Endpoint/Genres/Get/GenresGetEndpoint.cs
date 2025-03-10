﻿using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Genres.Get
{
    [Tags("Genres")]
    public class GenresGetEndpoint : MyBaseEndpoint<NoRequest, GenresGetResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GenresGetEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("GenresGet")]
        public override async Task<GenresGetResponse> Obradi([FromQuery] NoRequest request, CancellationToken cancellationToken = default)
        {
            var genres = await _applicationDbContext.Genre.Select(x => new GenresGetResponseGenre()
            {
                ID = x.ID,
                Name = x.Name
            }).ToListAsync();

            return new GenresGetResponse()
            {
                Genres = genres
            };
        }
    }
}
