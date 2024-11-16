using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GameShop.Endpoint.Users.Get
{
    [Tags("Users")]
    public class UserGetEndpoint : MyBaseEndpoint<UserGetRequest, UserGetResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public UserGetEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }
        [HttpGet("UsersGet")]
        public override async Task<UserGetResponse> Obradi([FromQuery] UserGetRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.jelLogiran())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var users = await _applicationDbContext.Korisnik.Include(kn => kn.KNalog).Where(k => k.KNalog.isKorisnik == true && k.KNalog.isDeleted == false && k.KNalog.isBlackList == request.isBlackList).Select(x => new UserGetResponseUser()
            {
                ID = x.Id,
                Name = x.Ime,
                Surname = x.Prezime,
                Username = x.KNalog.KorisnickoIme,
                Email = x.KNalog.Email
            }).ToListAsync();


            return new UserGetResponse()
            {
                Users = users
            };
        }
    }
}
