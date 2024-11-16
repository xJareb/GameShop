namespace GameShop.Endpoint.Users.Delete
{
    public class UserDeleteRequest
    {
        public int ID { get; set; }
        public bool isBlackList { get; set; }
    }
}
