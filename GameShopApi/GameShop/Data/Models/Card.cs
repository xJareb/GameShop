using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Data.Models
{
    public class Card
    {
        public int ID { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        [ForeignKey(nameof(User))]
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
