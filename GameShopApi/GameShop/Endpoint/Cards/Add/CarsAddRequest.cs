namespace GameShop.Endpoint.Cards.Add
{
    public class CarsAddRequest
    {
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int UserID { get; set; }
    }
}
