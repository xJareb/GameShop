using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.Users.Add
{
    [Tags("Users")]
    [Route("UserAdd")]
    public class UserAddEndpoint : MyBaseEndpoint<UserAddRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UserAddEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost]
        public override async Task<NoResponse> Obradi([FromBody] UserAddRequest request, CancellationToken cancellationToken = default)
        {
            var userAccount = new KorisnickiNalog()
            {
                KorisnickoIme = request.Username,
                Email = request.Email,
                Lozinka = LozinkaHasher.HashPassword(request.Password),
                DatumRodjenja = request.BirhtDate,
                isAdmin = false,
                isKorisnik = true
            };

            var checkDoubleName = _applicationDbContext.KorisnickiNalog.Where(kn => kn.KorisnickoIme == request.Username).FirstOrDefault();
            if (checkDoubleName != null)
                throw new Exception($"{HttpStatusCode.Conflict}");

            var checkDoubleEmail = _applicationDbContext.KorisnickiNalog.Where(e => e.Email == request.Email).FirstOrDefault();
            if (checkDoubleEmail != null)
                throw new Exception($"{HttpStatusCode.Conflict}");

            _applicationDbContext.KorisnickiNalog.Add(userAccount);
            await _applicationDbContext.SaveChangesAsync();

            var user = new Data.Models.Korisnik()
            {
                Ime = request.Name,
                Prezime = request.Surname,
                KorisnickiNalogID = userAccount.Id
            };
            _applicationDbContext.Korisnik.Add(user);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse
            {

            };
        }
    }
}
