using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.AuthGoogle.Login
{
    [Tags("GoogleAuth")]
    public class AuthGoogleLoginEndpoint : MyBaseEndpoint<AuthGoogleLoginReuqest, MyAuthInfo>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AuthGoogleLoginEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost("Login-google")]
        public override async Task<MyAuthInfo> Obradi([FromQuery] AuthGoogleLoginReuqest request, CancellationToken cancellationToken = default)
        {
            var loggedUser = await _applicationDbContext.User.Include(kn => kn.UserAccount)
                .FirstOrDefaultAsync(k =>
                    k.UserAccount.Email == request.Email && k.UserAccount.isGoogleProvider == true);

            if (loggedUser == null)
            {
                return new MyAuthInfo(null);
            }
            string randomString = TokenGenerator.Generate(10);

            var newToken = new AuthenticationToken()
            {
                ipAdress = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                value = randomString,
                UserAccount = loggedUser.UserAccount,
                TimeOfRecording = DateTime.Now,
                UserID = loggedUser.ID,
                ExpirationTime = DateTime.Now.AddMinutes(30)
            };
            _applicationDbContext.Add(newToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new MyAuthInfo(newToken);
        }
    }
}
