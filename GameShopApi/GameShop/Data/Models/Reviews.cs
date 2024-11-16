using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Data.Models
{
    public class Reviews
    {
        [Key]
        public int ID { get; set; }
        public string Content { get; set; }
        public int Grade { get; set; }
        [ForeignKey(nameof(User))]
        public int UserID { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(Games))]
        public int GameID { get; set; }
        public Games Games { get; set; }
    }
}
