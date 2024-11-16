using GameShop.Data.Models;

namespace GameShop.Endpoint.Users.GetLogged
{
    public class UserGetLoggedResponse
    {
        public List<UserGetLoggedResponseUser> User { get; set; }
    }
    public class UserGetLoggedResponseUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime? DateBirth { get; set; }
        public byte[] PhotoBytes { get; set; }
        public int NumberOfPurchase { get; set; }
        public string GooglePhoto { get; set; }
    }
}
