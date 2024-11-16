namespace GameShop.Endpoint.ShoppingCart.Add
{
    public class ShoppingCartAddRequest
    {
        public int UserID { get; set; }
        public int GameID { get; set; }
        public int Quantity { get; set; }
    }
}
