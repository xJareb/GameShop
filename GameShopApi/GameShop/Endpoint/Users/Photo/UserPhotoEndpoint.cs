using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using GameShop.Helper.Services;

namespace GameShop.Endpoint.Users.Photo
{
    [Tags("Users")]
    public class UserPhotoEndpoint : MyBaseEndpoint<UserPhotoRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly TokenValidation _tokenValidation;
        private readonly MyAuthService _authService;

        public UserPhotoEndpoint(ApplicationDbContext applicationDbContext, TokenValidation tokenValidation, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _tokenValidation = tokenValidation;
            _authService = authService;
        }
        [HttpPut("UserPhoto")]
        public override async Task<NoResponse> Obradi([FromForm] UserPhotoRequest request, CancellationToken cancellationToken = default)
        {
            if (!_tokenValidation.checkTokenValidation())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized} : Token expired");
            }

            var logedUser = _authService.GetUser();
            if (logedUser == null)
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            var loggedUserID = logedUser.AuthenticationToken.UserID;

            var user = _applicationDbContext.User.Where(k => k.ID == loggedUserID).FirstOrDefault();
            if (user == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            if (request.Photo != null && request.Photo.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await request.Photo.CopyToAsync(memoryStream);

                    user.PhotoBytes = memoryStream.ToArray();

                    _applicationDbContext.User.Update(user);
                    await _applicationDbContext.SaveChangesAsync();
                }
                return new NoResponse();
            }
            return new NoResponse();
        }
    }
}
