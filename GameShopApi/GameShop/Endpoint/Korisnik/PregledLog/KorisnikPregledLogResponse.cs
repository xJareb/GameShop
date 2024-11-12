using GameShop.Data.Models;

namespace GameShop.Endpoint.Korisnik.PregledLog
{
    public class KorisnikPregledLogResponse
    {
        public List<KorisnikPregledLogResponseKorisnik> Korisnik {  get; set; }
    }
    public class KorisnikPregledLogResponseKorisnik
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        //public KorisnickiNalog KorisnickiNalog { get; set; }
        public string KorisnickoIme { get; set; }
        public string Email { get; set; }
        public DateTime? DatumRodjenja { get; set; }
        public byte[] Slika { get; set; }
        public int BrojNarudzbi {  get; set; }
        public string GoogleSlika { get; set; }
    }
}
