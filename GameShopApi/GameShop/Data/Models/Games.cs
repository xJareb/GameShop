using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Data.Models
{
    public class Games
    {
        [Key]
        public int ID { get; set; }
        public string Name {  get; set; }
        [ForeignKey(nameof(Genre))]
        public int GenreID { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseDate {  get; set; }
        public string Photo { get; set; }
        public string Publisher {  get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public float PercentageDiscount { get; set; }
        public float? ActionPrice {  get; set; }
        public bool Highlighted { get; set; } = false;
        public List<Purchases> Purchases { get; set; } = new List<Purchases>();
    }
}
