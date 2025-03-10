﻿using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GameShop.Endpoint.Users.Update
{
    [Tags("Users")]
    public class UserUpdateEndpoint : MyBaseEndpoint<UserUpdateRequest, UserUpdateResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;
        private readonly TokenValidation _tokenValidation;

        public UserUpdateEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService, TokenValidation tokenValidation)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
            _tokenValidation = tokenValidation;
        }

        [HttpPut("UserUpdate")]
        public override async Task<UserUpdateResponse> Obradi(UserUpdateRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.isLogged())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            if (!_tokenValidation.checkTokenValidation())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized} : Token expired");
            }
            var user = _applicationDbContext.User.Include(kn => kn.UserAccount).Where(k => k.ID == request.UserID).FirstOrDefault();
            if (user == null)
                throw new Exception($"{HttpStatusCode.NotFound}");

            user.Name = request.Name;
            user.Surname = request.Surname;
            user.UserAccount.Email = request.Email;

            if (LozinkaHasher.VerifikujLozinku(request.Password, user.UserAccount.Password))
            {
                _applicationDbContext.Update(user);
                await _applicationDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Wrong password");
            }

            return new UserUpdateResponse()
            {
                User = user
            };
        }
    }
}
