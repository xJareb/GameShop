using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Data.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(User))]
        public int UserID { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(Game))]
        public int GameID { get; set; }
        public Games Game { get; set; }
        public int Quantity {  get; set; }
    }
}
