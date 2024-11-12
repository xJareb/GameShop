namespace GameShop.Endpoint.Recenzije.PregledPoIgrici
{
    public class PregledPoIgriciResponse
    {
        public List<PregledPoIgriciResponsePregled> Recenzije {  get; set; }
    }
    public class PregledPoIgriciResponsePregled
    {
        public int KorisnikID { get; set; }
        public string Sadrzaj { get; set; }
        public int Ocjena { get; set; }
        public byte[]? Slika { get; set; }
        public string KorisnickoIme { get; set; }
        public int IgricaID { get; set; }
        public string Igrica {  get; set; }
    }
}
