using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Data.Models
{
    public class AuthenticationToken
    {
        [Key]
        public int ID { get; set; }
        public string value {  get; set; }
        public int UserID { get; set; }
        [ForeignKey(nameof(UserAccount))]
        public int UserAccountID { get; set; }
        public UserAccount UserAccount { get; set; }
        public DateTime TimeOfRecording { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string? ipAdress { get; set; }
    }
}
