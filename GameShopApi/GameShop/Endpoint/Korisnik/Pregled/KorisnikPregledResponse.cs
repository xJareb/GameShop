namespace GameShop.Endpoint.Korisnik.Pregled
{
    public class KorisnikPregledResponse
    {
        public List<KorisnikPregledResponseKorisnik> Korisnici { get; set; }
    }
    public class KorisnikPregledResponseKorisnik
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Email { get; set; }
    }
}
