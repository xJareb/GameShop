namespace GameShop.Endpoint.Zanrovi.Pretrazi
{
    public class ZanroviPretraziResponse
    {
        public List<ZanroviPretraziResponseZanr> Zanrovi {  get; set; }
    }
    public class ZanroviPretraziResponseZanr
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
    }
}
