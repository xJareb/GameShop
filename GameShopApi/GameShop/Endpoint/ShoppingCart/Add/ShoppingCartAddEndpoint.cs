using GameShop.Data;
using GameShop.Data.Models;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.ShoppingCart.Add
{
    [Tags("ShoppingCart")]
    public class ShoppingCartAddEndpoint : MyBaseEndpoint<ShoppingCartAddRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;
        public ShoppingCartAddEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }
        [HttpPost("ShoppingCartAdd")]
        public override async Task<NoResponse> Obradi([FromBody] ShoppingCartAddRequest request, CancellationToken cancellationToken = default)
        {
            if (!_authService.jelLogiran())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var checkDuplicates = _applicationDbContext.Korpa.Where(pd => pd.KorisnikID == request.UserID && pd.IgricaID == request.UserID).FirstOrDefault();
            if (checkDuplicates == null)
            {
                var novaIgrica = new Data.Models.Korpa()
                {
                    KorisnikID = request.UserID,
                    IgricaID = request.GameID,
                    Kolicina = request.Quantity
                };
                if (novaIgrica.Kolicina == 0)
                {
                    throw new Exception($"{HttpStatusCode.BadRequest}");
                }
                _applicationDbContext.Korpa.Add(novaIgrica);

            }
            else
            {
                checkDuplicates.Kolicina = checkDuplicates.Kolicina + request.Quantity;
                _applicationDbContext.Korpa.Update(checkDuplicates);
            }
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
