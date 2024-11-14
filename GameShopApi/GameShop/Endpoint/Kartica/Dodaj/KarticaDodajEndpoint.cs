using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace GameShop.Endpoint.Kartica.Dodaj
{
    [Tags("Kartica")]
    public class KarticaDodajEndpoint : MyBaseEndpoint<KarticaDodajRequest,NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public KarticaDodajEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }
        [HttpPost("DodajKarticu")]
        public override async Task<NoResponse> Obradi(KarticaDodajRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.jelLogiran())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            byte[] key = Encoding.UTF8.GetBytes("1234567890123456");
            byte[] iv = Encoding.UTF8.GetBytes("1234567890123456");

            var novaKartica = new Data.Models.Kartica()
            {
                BrojKartice = EncryptDecryptString.EncryptString(request.BrojKartice, key, iv),
                Istek = DatumHasher.HashDate(request.DatumIsteka),
                KorisnikID = request.KorisnikID
            };
            _applicationDbContext.Kartica.Add(novaKartica);
            await _applicationDbContext.SaveChangesAsync();
            
            return new NoResponse();
        }
    }
}
