namespace GameShop.Endpoint.Igrice.Pretrazi
{
    public class IgricePretraziResponse
    {
        public List<IgricePretraziResponseIgrice> Igrice {  get; set; }
    }
    public class IgricePretraziResponseIgrice
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Zanr { get; set; }
        public DateTime DatumIzlaska { get; set; }
        public string Slika { get; set; }
        public string Izdavac { get; set; }
        public string Opis { get; set; }
        public float Cijena { get; set; }
        public float AkcijskaCijena { get; set; }
    }
}
