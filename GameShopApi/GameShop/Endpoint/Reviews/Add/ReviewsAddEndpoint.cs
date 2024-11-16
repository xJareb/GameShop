using GameShop.Data;
using GameShop.Helper;
using GameShop.Helper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShop.Endpoint.Reviews.Add
{
    [Tags("Reviews")]
    public class ReviewsAddEndpoint : MyBaseEndpoint<ReviewsAddRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public ReviewsAddEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpPost("ReviewAdd")]
        public override async Task<NoResponse> Obradi([FromBody] ReviewsAddRequest request, CancellationToken cancellationToken = default)
        {
            if (!_myAuthService.jelLogiran())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var checkDuplicates = _applicationDbContext.Recenzije.Where(r => request.UserID == r.KorisnikID && request.UserID == r.IgricaID).FirstOrDefault();
            if (checkDuplicates != null)
                throw new Exception($"{HttpStatusCode.Conflict}");
            var newReview = new Data.Models.Recenzije()
            {
                Sadrzaj = request.Content,
                Ocjena = request.Grade,
                KorisnikID = request.UserID,
                IgricaID = request.GameID,
            };

            _applicationDbContext.Recenzije.Add(newReview);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
