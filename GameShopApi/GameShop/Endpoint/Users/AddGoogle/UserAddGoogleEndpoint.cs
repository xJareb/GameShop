using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Endpoint.Users.AddGoogle
{
    [Tags("Users")]
    public class UserAddGoogleEndpoint : MyBaseEndpoint<UserAddGoogleRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserAddGoogleEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost("UserGoogleAdd")]
        public override async Task<NoResponse> Obradi([FromBody] UserAddGoogleRequest request, CancellationToken cancellationToken = default)
        {
            var checkEmail = _applicationDbContext.KorisnickiNalog.Where(kn => kn.Email == request.Email).FirstOrDefault();
            var googleUser = new KorisnickiNalog()
            {
                Email = request.Email,
                isGoogleProvider = request.isGoogleProvider,
                isKorisnik = true
            };

            if (checkEmail == null)
            {
                _applicationDbContext.KorisnickiNalog.Add(googleUser);
                await _applicationDbContext.SaveChangesAsync();
            }
            var user = new Data.Models.Korisnik()
            {
                Ime = request.Name,
                GoogleSlika = request.Photo,
                KorisnickiNalogID = googleUser.Id
            };

            if (checkEmail == null)
            {
                _applicationDbContext.Korisnik.Add(user);
                await _applicationDbContext.SaveChangesAsync();
            }
            return new NoResponse();
        }
    }
}
