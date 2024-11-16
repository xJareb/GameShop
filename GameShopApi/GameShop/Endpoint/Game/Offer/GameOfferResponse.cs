namespace GameShop.Endpoint.Game.Offer
{
    public class GameOfferResponse
    {
        public List<GameOfferResponseGame> Games { get; set; }
    }
    public class GameOfferResponseGame
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public float PercentageDiscount { get; set; }
        public float? ActionPrice { get; set; }
    }
}
