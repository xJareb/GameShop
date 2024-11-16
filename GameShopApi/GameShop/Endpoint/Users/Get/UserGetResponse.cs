namespace GameShop.Endpoint.Users.Get
{
    public class UserGetResponse
    {
        public List<UserGetResponseUser> Users { get; set; }
    }
    public class UserGetResponseUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
