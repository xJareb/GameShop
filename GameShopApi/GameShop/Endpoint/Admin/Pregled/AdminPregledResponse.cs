namespace GameShop.Endpoint.Admin.Pregled
{
    public class AdminPregledResponse
    {
        public List<AdminPregledResponseAdmin> Admin {  get; set; }
    }
    public class AdminPregledResponseAdmin
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Email { get; set; }
        public byte[] Slika { get; set; }
    }
}
