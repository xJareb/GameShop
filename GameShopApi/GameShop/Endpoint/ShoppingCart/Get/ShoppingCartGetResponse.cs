namespace GameShop.Endpoint.ShoppingCart.Get
{
    public class ShoppingCartGetResponse
    {
        public List<ShoppingCartGetResponseShoppingCart> Cart { get; set; }
    }
    public class ShoppingCartGetResponseShoppingCart
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public float Price { get; set; }
        public float ActionPrice { get; set; }
        public int Quantity { get; set; }
    }
}
