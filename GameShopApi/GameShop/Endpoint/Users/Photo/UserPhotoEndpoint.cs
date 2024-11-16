using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;

namespace GameShop.Endpoint.Users.Photo
{
    [Tags("Users")]
    public class UserPhotoEndpoint : MyBaseEndpoint<UserPhotoRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserPhotoEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPut("UserPhoto")]
        public override async Task<NoResponse> Obradi([FromForm] UserPhotoRequest request, CancellationToken cancellationToken = default)
        {
            var logedUser = _applicationDbContext.AutentifikacijaToken.FirstOrDefault();
            if (logedUser == null)
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            var loggedUserID = logedUser.KorisnickiNalogID;

            var user = _applicationDbContext.Korisnik.Where(k => k.Id == loggedUserID).FirstOrDefault();
            if (user == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            if (request.Photo != null && request.Photo.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await request.Photo.CopyToAsync(memoryStream);

                    user.Slika = memoryStream.ToArray();

                    _applicationDbContext.Korisnik.Update(user);
                    await _applicationDbContext.SaveChangesAsync();
                }
                return new NoResponse();
            }
            return new NoResponse();
        }
    }
}
