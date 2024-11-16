namespace GameShop.Endpoint.Game.Add
{
    public class GameAddRequest
    {
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
