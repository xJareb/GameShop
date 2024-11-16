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
            var userAccount = new UserAccount()
            {
                Username = request.Username,
                Email = request.Email,
                Password = LozinkaHasher.HashPassword(request.Password),
                BirthDate = request.BirhtDate,
                isAdmin = false,
                isUser = true
            };

            var checkDoubleName = _applicationDbContext.UserAccounts.Where(kn => kn.Username == request.Username).FirstOrDefault();
            if (checkDoubleName != null)
                throw new Exception($"{HttpStatusCode.Conflict}");

            var checkDoubleEmail = _applicationDbContext.UserAccounts.Where(e => e.Email == request.Email).FirstOrDefault();
            if (checkDoubleEmail != null)
                throw new Exception($"{HttpStatusCode.Conflict}");

            _applicationDbContext.UserAccounts.Add(userAccount);
            await _applicationDbContext.SaveChangesAsync();

            var user = new Data.Models.User()
            {
                Name = request.Name,
                Surname = request.Surname,
                UserAccountID = userAccount.ID
            };
            _applicationDbContext.User.Add(user);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse
            {

            };
        }
    }
}
