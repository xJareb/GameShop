using GameShop.Data;
using GameShop.Endpoint.Korisnik.PregledLog;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GameShop.Endpoint.Users.GetLogged
{
    [Tags("Users")]
    public class UserGetLoggedEndpoint : MyBaseEndpoint<UserGetLoggedRequest, UserGetLoggedResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public UserGetLoggedEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }
        [HttpGet("GetLogged")]
        public override async Task<UserGetLoggedResponse> Obradi([FromQuery] UserGetLoggedRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.isLogged())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var purchaseCount = _applicationDbContext.Purchases.Where(k => k.UserID == request.LoggedUserID).Count();
            var loggedUser = await _applicationDbContext.User.Include(k => k.UserAccount).Where(lk => lk.ID == request.LoggedUserID).Select(x => new UserGetLoggedResponseUser()
            {
                ID = x.ID,
                Name = x.Name,
                Surname = x.Surname,
                Username = x.UserAccount.Username,
                Email = x.UserAccount.Email,
                DateBirth = x.UserAccount.BirthDate,
                PhotoBytes = x.PhotoBytes,
                NumberOfPurchase = purchaseCount,
                GooglePhoto = x.GooglePhoto
            }).ToListAsync();
            return new UserGetLoggedResponse()
            {
                User = loggedUser
            };
        }
    }
}
