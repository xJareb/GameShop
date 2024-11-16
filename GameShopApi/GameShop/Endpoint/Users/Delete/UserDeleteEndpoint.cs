using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GameShop.Endpoint.Users.Delete
{
    [Tags("Users")]
    public class UserDeleteEndpoint : MyBaseEndpoint<UserDeleteRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public UserDeleteEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }
        [HttpPut("UserDelete")]
        public override async Task<NoResponse> Obradi([FromQuery] UserDeleteRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.jelAdmin())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var user = _applicationDbContext.Korisnik.Include(kn => kn.KNalog).Where(k => k.Id == request.ID).FirstOrDefault();
            if (user == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            if (request.isBlackList == false)
            {
                user.KNalog.isDeleted = true;
            }
            else
            {
                user.KNalog.isBlackList = true;
            }


            _applicationDbContext.Update(user);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
