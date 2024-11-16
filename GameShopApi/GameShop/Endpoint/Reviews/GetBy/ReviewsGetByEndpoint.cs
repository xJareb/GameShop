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
            var reviews = await _applicationDbContext.Recenzije.Where(r => request.GameID == r.IgricaID || request.GameID == 0).Select(x => new ReviewsGetByResponseReview()
            {
                UserID = x.KorisnikID,
                Content = x.Sadrzaj,
                Grade = x.Ocjena,
                PhotoBytes = x.Korisnik.Slika,
                Username = x.Korisnik.KNalog.KorisnickoIme,
                GameID = x.IgricaID,
                Game = x.Igrice.Naziv
            }).ToListAsync();

            return new ReviewsGetByResponse
            {
                Reviews = reviews
            };
        }
    }
}
