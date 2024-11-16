namespace GameShop.Endpoint.Admin.Add
{
    public class AdminAddResponse
    {
        public List<AdminAddResponseAdmin> Admin { get; set; }
    }
    public class AdminAddResponseAdmin
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] PhotoBytes { get; set; }
    }
}
