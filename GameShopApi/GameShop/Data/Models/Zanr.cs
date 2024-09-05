using System.ComponentModel.DataAnnotations;

namespace GameShop.Data.Models
{
    public class Zanr
    {
        [Key]
        public int Id { get; set; }
        public string Naziv {  get; set; }
    }
}
