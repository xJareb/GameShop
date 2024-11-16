using System.ComponentModel.DataAnnotations;

namespace GameShop.Data.Models
{
    public class UserAccount
    {
        [Key]
        public int ID { get; set; }
        public string? Username { get; set; }
        public string Email {  get; set; }
        public string? Password { get; set; }
        public DateTime? BirthDate {  get; set; }
        public bool isAdmin { get; set; }
        public bool isUser { get; set; }
        public bool isDeleted {  get; set; } = false;
        public bool isBlackList { get; set; } = false;
        public bool isGoogleProvider {  get; set; }

    }
}
