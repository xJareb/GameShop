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
            if (!_authService.isLogged())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var users = await _applicationDbContext.User.Include(kn => kn.UserAccount).Where(k => k.UserAccount.isUser == true && k.UserAccount.isDeleted == false && k.UserAccount.isBlackList == request.isBlackList).Select(x => new UserGetResponseUser()
            {
                ID = x.ID,
                Name = x.Name,
                Surname = x.Surname,
                Username = x.UserAccount.Username,
                Email = x.UserAccount.Email
            }).ToListAsync();


            return new UserGetResponse()
            {
                Users = users
            };
        }
    }
}
