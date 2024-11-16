namespace GameShop.Endpoint.Users.Add
{
    public class UserAddRequest
    {
        public string Name { get; set; }
        public string? Surname { get; set; }
        public string? Username { get; set; }
        public string Email { get; set; }
        public DateTime? BirhtDate { get; set; }
        public string? Password { get; set; }

    }
}
