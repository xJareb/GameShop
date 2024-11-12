using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Endpoint.Auth.Odjava
{
    [Tags("Auth")]
    public class AuthOdjavaEndpoint : MyBaseEndpoint<NoRequest,NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;
        public AuthOdjavaEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }
        [HttpPost("Odjavi-se")]
        public override async Task<NoResponse> Obradi([FromBody] NoRequest request, CancellationToken cancellationToken)
        {
           AutentifikacijaToken? autentifikacijaToken = _authService.GetAuthInfo().autentifikacijaToken;

            if (autentifikacijaToken == null)
                return new NoResponse();

            _applicationDbContext.Remove(autentifikacijaToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return new NoResponse();
        }
    }
}
