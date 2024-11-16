using GameShop.Data;
using GameShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace GameShop.Helper.Services
{
    public class MyAuthService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyAuthService(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public bool isLogged()
        {
            return GetAuthInfo().isLogged;
        }
        public bool isUser()
        {
            return GetAuthInfo().UserAccount?.isUser ?? false;
        }
        public bool isAdmin()
        {
            return GetAuthInfo().UserAccount?.isAdmin ?? false;
        }
        public MyAuthInfo GetUser()
        {
            var authenticationUser = _applicationDbContext.AuthenticationToken.Include(x => x.UserAccount).FirstOrDefault();
            return new MyAuthInfo(authenticationUser);
        }
        public MyAuthInfo GetAuthInfo()
        {
            string? authToken = _httpContextAccessor.HttpContext!.Request.Headers["my-auth-token"];

            AuthenticationToken? authenticationToken = _applicationDbContext.AuthenticationToken
                .Include(x => x.UserAccount)
                .SingleOrDefault(x => x.value == authToken);

            return new MyAuthInfo(authenticationToken);
        }
    }
    public class MyAuthInfo
    {
        public MyAuthInfo(AuthenticationToken? authenticationToken)
        {
            this.AuthenticationToken = authenticationToken;
        }

        [JsonIgnore]
        public UserAccount? UserAccount => AuthenticationToken?.UserAccount;
        public AuthenticationToken? AuthenticationToken { get; set; }

        public bool isLogged => UserAccount != null;

    }
}
