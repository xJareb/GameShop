namespace GameShop.Endpoint.Game.Get
{
    public class GameGetResponse
    {
        public List<GameGetResponseGame> Game { get; set; }
    }
    public class GameGetResponseGame
    {
        public int ID { get; set; }
        public string Name { get; set; }
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
