namespace GameShop.Endpoint.Cards.Get
{
    public class CardsGetResponse
    {
        public List<CardsGetResponseCard> Cards { get; set; }
    }
    public class CardsGetResponseCard
    {
        public string CardNumber { get; set; }
    }
}
