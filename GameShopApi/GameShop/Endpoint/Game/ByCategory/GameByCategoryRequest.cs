namespace GameShop.Endpoint.Game.ByCategory
{
    public class GameByCategoryRequest
    {
        public int GenreID { get; set; }
        public int FirstPrice { get; set; } = 1;
        public int LastPrice { get; set; } = 100;
        public string? Sorting { get; set; }
        public string? GameName { get; set; }
    }
}
