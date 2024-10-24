namespace GameShop.Endpoint.Recenzije.PregledPoIgrici
{
    public class PregledPoIgriciResponse
    {
        public List<PregledPoIgriciResponsePregled> Recenzije {  get; set; }
    }
    public class PregledPoIgriciResponsePregled
    {
        public string Sadrzaj { get; set; }
        public int Ocjena { get; set; }
        public byte[]? Slika { get; set; }
        public string KorisnickoIme { get; set; }
    }
}
