namespace GameShop.Endpoint.Reviews.GetBy
{
    public class ReviewsGetByResponse
    {
        public List<ReviewsGetByResponseReview> Reviews { get; set; }
    }
    public class ReviewsGetByResponseReview
    {
        public int UserID { get; set; }
        public string Content { get; set; }
        public int Grade { get; set; }
        public byte[]? PhotoBytes { get; set; }
        public string GooglePhoto { get; set; }
        public string Username { get; set; }
        public int GameID { get; set; }
        public string Game { get; set; }
        
    }
}
