using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Endpoint.Auth.Login;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Auth.Prijava
{
    [Tags("Auth")]
    public class AuthLoginEndpoint : MyBaseEndpoint<AuthLoginResponse, MyAuthInfo>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AuthLoginEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost("Login")]
        public override async Task<MyAuthInfo> Obradi([FromBody]AuthLoginResponse request, CancellationToken cancellationToken = default)
        {
            var loggedUser = await _applicationDbContext.User.Include(kn=>kn.UserAccount)
                .FirstOrDefaultAsync(k =>
                    k.UserAccount.Username == request.Username, cancellationToken);

            if (!LozinkaHasher.VerifikujLozinku(request.Password, loggedUser.UserAccount.Password)){
                throw new Exception("Not found");
            }

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
                UserID = loggedUser.ID
            };
            _applicationDbContext.Add(newToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new MyAuthInfo(newToken);
        }
    }
}
