namespace GameShop.Endpoint.Game.ByCategory
{
    public class GameByCategoryResponse
    {
        public List<GameByCategoryResponseGame> Games { get; set; }
    }
    public class GameByCategoryResponseGame
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int GenreID { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Photo { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public float PercentageDiscount { get; set; }
        public float ActionPrice { get; set; }
    }
}
