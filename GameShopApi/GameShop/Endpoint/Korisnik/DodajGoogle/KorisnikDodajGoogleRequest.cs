namespace GameShop.Endpoint.Korisnik.DodajGoogle
{
    public class KorisnikDodajGoogleRequest
    {
        public string Ime { get; set; }
        public string Email { get; set; }
        public string Slika { get; set; }
        public bool isGoogleProvider { get; set; }
    }
}
