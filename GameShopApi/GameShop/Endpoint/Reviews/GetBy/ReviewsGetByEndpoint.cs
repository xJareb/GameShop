using GameShop.Data;
using GameShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Endpoint.Reviews.GetBy
{
    [Tags("Reviews")]
    public class ReviewsGetByEndpoint : MyBaseEndpoint<ReviewsGetByRequest, ReviewsGetByResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ReviewsGetByEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("ReviewsGetBy")]
        public override async Task<ReviewsGetByResponse> Obradi([FromQuery] ReviewsGetByRequest request, CancellationToken cancellationToken = default)
        {
            var reviews = await _applicationDbContext.Reviews.Where(r => request.GameID == r.GameID || request.GameID == 0).Select(x => new ReviewsGetByResponseReview()
            {
                UserID = x.UserID,
                Content = x.Content,
                Grade = x.Grade,
                PhotoBytes = x.User.PhotoBytes,
                GooglePhoto = x.User.GooglePhoto,
                Username = x.User.UserAccount.Username,
                GameID = x.GameID,
                Game = x.Games.Name
            }).ToListAsync();

            return new ReviewsGetByResponse
            {
                Reviews = reviews
            };
        }
    }
}
