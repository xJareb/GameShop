using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GameShop.Endpoint.Admin.Add
{
    [Tags("Admin")]
    public class AdminAddEndpoint : MyBaseEndpoint<AdminAddRequest, AdminAddResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public AdminAddEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }
        [HttpGet("GetAdmin")]
        public override async Task<AdminAddResponse> Obradi([FromQuery] AdminAddRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.jelAdmin())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var admin = await _applicationDbContext.Korisnik.Include(kn => kn.KNalog).Where(a => a.Id == request.ID && a.KNalog.isAdmin == true).Select(x => new AdminAddResponseAdmin()
            {
                Name = x.Ime,
                Surname = x.Prezime,
                Username = x.KNalog.KorisnickoIme,
                Email = x.KNalog.Email,
                PhotoBytes = x.Slika
            }).ToListAsync();


            return new AdminAddResponse()
            {
                Admin = admin
            };
        }
    }
}
