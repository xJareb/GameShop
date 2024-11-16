using GameShop.Data.Models;

namespace GameShop.Endpoint.Purchases.Get
{
    public class PurchasesGetResponse
    {
        public List<PurchasesGetResponsePurchase> Purchases { get; set; }
    }
    public class PurchasesGetResponsePurchase
    {
        public int ID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int UserID { get; set; }
        public string User { get; set; }
        public List<PurchasedGames> Games { get; set; }
    }
    public class PurchasedGames
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public float ActionPrice { get; set; }
    }
}
