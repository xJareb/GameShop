using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Endpoint.Auth.Odjava
{
    [Tags("Auth")]
    public class AuthLogoutEndpoint : MyBaseEndpoint<NoRequest,NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;
        public AuthLogoutEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }
        [HttpPost("Logout")]
        public override async Task<NoResponse> Obradi([FromBody] NoRequest request, CancellationToken cancellationToken)
        {
           AuthenticationToken? authenticationToken = _authService.GetAuthInfo().AuthenticationToken;

            if (authenticationToken == null)
                return new NoResponse();

            _applicationDbContext.Remove(authenticationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return new NoResponse();
        }
    }
}
