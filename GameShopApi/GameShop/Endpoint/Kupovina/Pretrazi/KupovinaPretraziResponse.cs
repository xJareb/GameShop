using GameShop.Data.Models;

namespace GameShop.Endpoint.Kupovina.Pretrazi
{
    public class KupovinaPretraziResponse
    {
        public List<KupovinaPretraziResponseKupovina> Kupovine { get; set; }
    }
    public class KupovinaPretraziResponseKupovina
    {
        public int ID { get; set; }
        public DateTime DatumKupovine { get; set; }
        public int KorisnikID { get; set; }
        public List<KupljeneIgrice> Igrice { get; set; }
    }
    public class KupljeneIgrice
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Slika { get; set; }
        public float AkcijskaCijena { get; set; }
    }
}
