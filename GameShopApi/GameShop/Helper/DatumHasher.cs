using System.Security.Cryptography;
using System.Text;

namespace GameShop.Helper
{
    public class DatumHasher
    {
        public static string HashDate(DateTime date)
        {
            string datumString = date.ToString("o");

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(datumString));
                string hashString = BitConverter.ToString(hashBytes).Replace("-", "");

                return hashString;
            }
        }
    }
}
