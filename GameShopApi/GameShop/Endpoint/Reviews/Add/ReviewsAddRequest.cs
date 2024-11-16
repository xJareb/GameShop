namespace GameShop.Endpoint.Reviews.Add
{
    public class ReviewsAddRequest
    {
        public int UserID { get; set; }
        public int GameID { get; set; }
        public int Grade { get; set; }
        public string Content { get; set; }
    }
}
