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
            if (!_authService.jelLogiran())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var purchaseCount = _applicationDbContext.Kupovine.Where(k => k.KorisnikID == request.LoggedUserID).Count();
            var loggedUser = await _applicationDbContext.Korisnik.Include(k => k.KNalog).Where(lk => lk.Id == request.LoggedUserID).Select(x => new UserGetLoggedResponseUser()
            {
                ID = x.Id,
                Name = x.Ime,
                Surname = x.Prezime,
                Username = x.KNalog.KorisnickoIme,
                Email = x.KNalog.Email,
                DateBirth = x.KNalog.DatumRodjenja,
                PhotoBytes = x.Slika,
                NumberOfPurchase = purchaseCount,
                GooglePhoto = x.GoogleSlika
            }).ToListAsync();
            return new UserGetLoggedResponse()
            {
                User = loggedUser
            };
        }
    }
}
