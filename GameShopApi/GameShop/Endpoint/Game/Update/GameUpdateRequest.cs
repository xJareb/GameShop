namespace GameShop.Endpoint.Game.Update
{
    public class GameUpdateRequest
    {
        public int GameID { get; set; }
        public string Name { get; set; }
        public int GenreID { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Photo { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public float PercentageDiscount { get; set; }
    }
}
