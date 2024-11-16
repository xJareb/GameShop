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
            if (!_authService.isAdmin())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var admin = await _applicationDbContext.User.Include(kn => kn.UserAccount).Where(a => a.ID == request.ID && a.UserAccount.isAdmin == true).Select(x => new AdminAddResponseAdmin()
            {
                Name = x.Name,
                Surname = x.Surname,
                Username = x.UserAccount.Username,
                Email = x.UserAccount.Email,
                PhotoBytes = x.PhotoBytes
            }).ToListAsync();


            return new AdminAddResponse()
            {
                Admin = admin
            };
        }
    }
}
