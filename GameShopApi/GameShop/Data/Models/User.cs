using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameShop.Data.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Surname { get; set; }
        public byte[]? PhotoBytes {  get; set; }
        public string? GooglePhoto {  get; set; } 
        [ForeignKey(nameof(UserAccount))]
        public int UserAccountID { get; set; }
        public UserAccount UserAccount { get; set; }

    }
}
