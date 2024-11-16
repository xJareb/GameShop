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
            if (!_myAuthService.isLogged())
            {
                throw new Exception($"{HttpStatusCode.Unauthorized}");
            }
            var checkDuplicates = _applicationDbContext.Reviews.Where(r => request.UserID == r.UserID && request.UserID == r.GameID).FirstOrDefault();
            if (checkDuplicates != null)
                throw new Exception($"{HttpStatusCode.Conflict}");
            var newReview = new Data.Models.Reviews()
            {
                Content = request.Content,
                Grade = request.Grade,
                UserID = request.UserID,
                GameID = request.GameID,
            };

            _applicationDbContext.Reviews.Add(newReview);
            await _applicationDbContext.SaveChangesAsync();

            return new NoResponse();
        }
    }
}
