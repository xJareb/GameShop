using GameShop.Data;
using GameShop.Helper.Services;

namespace GameShop.Helper
{
    public class TokenValidation
    {
        private readonly ApplicationDbContext _context;
        private readonly MyAuthService _authService;

        public TokenValidation(ApplicationDbContext context, MyAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public bool checkTokenValidation()
        {
            var user = _authService.GetUser();
            if (user == null || user.AuthenticationToken.ExpirationTime < DateTime.Now)
            {
                return false;
            }

            return true;
        }
    }
}
