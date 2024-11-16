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
            var checkEmail = _applicationDbContext.UserAccounts.Where(kn => kn.Email == request.Email).FirstOrDefault();
            var googleUser = new UserAccount()
            {
                Email = request.Email,
                isGoogleProvider = request.isGoogleProvider,
                isUser = true
            };

            if (checkEmail == null)
            {
                _applicationDbContext.UserAccounts.Add(googleUser);
                await _applicationDbContext.SaveChangesAsync();
            }
            var user = new Data.Models.User()
            {
                Name = request.Name,
                GooglePhoto = request.Photo,
                UserAccountID = googleUser.ID
            };

            if (checkEmail == null)
            {
                _applicationDbContext.User.Add(user);
                await _applicationDbContext.SaveChangesAsync();
            }
            return new NoResponse();
        }
    }
}
