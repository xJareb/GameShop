using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Data.Models
{
    public class Purchases
    {
        [Key]
        public int ID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public List<Games> Games { get; set; } = new List<Games>();
        [ForeignKey(nameof(User))]
        public int UserID { get; set; }
        public User User { get; set; }
    }
}

