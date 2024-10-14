namespace GameShop.Endpoint.Korpa.Pretrazi
{
    public class KorpaPretraziResponse
    {
        public List<KorpaPretraziResponseKorpa> Korpa { get; set; }
    }
    public class KorpaPretraziResponseKorpa
    {
        public int ID { get; set; }
        public string Naziv {  get; set; }
        public string Slika { get; set; }
        public float PravaCijena { get; set; }
        public float AkcijskaCijena { get; set; }
        public int Kolicina { get; set; }
    }
}
