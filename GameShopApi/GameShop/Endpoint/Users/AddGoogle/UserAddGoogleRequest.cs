namespace GameShop.Endpoint.Users.AddGoogle
{
    public class UserAddGoogleRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public bool isGoogleProvider { get; set; }
    }
}
