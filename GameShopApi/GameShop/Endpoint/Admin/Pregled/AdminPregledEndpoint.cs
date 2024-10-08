using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Admin.Pregled
{
    [Tags("Admin")]
    public class AdminPregledEndpoint : MyBaseEndpoint<AdminPregledRequest, AdminPregledResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AdminPregledEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("PretraziAdmina")]
        public override async Task<AdminPregledResponse> Obradi([FromQuery]AdminPregledRequest request, CancellationToken cancellationToken = default)
        {
            var admin = await _applicationDbContext.Korisnik.Include(kn=>kn.KNalog).Where(a=>a.Id == request.ID && a.KNalog.isAdmin == true).Select(x=>new AdminPregledResponseAdmin()
            {
                Ime = x.Ime,
                Prezime = x.Prezime,
                KorisnickoIme = x.KNalog.KorisnickoIme,
                Email = x.KNalog.Email,
                Slika = x.Slika
            }).ToListAsync();


            return new AdminPregledResponse()
            {
                Admin = admin
            };
        }
    }
}
