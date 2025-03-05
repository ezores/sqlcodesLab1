using System.Security.Cryptography;
using System.Text;

namespace Webflix.Services
{
    public static class PasswordHelper
    {
        public static string HashPassword(string plainText)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(plainText);
                byte[] hash = sha256.ComputeHash(bytes);

                var sb = new StringBuilder();
                foreach (var b in hash)
                {
                    sb.Append(b.ToString("x2")); // convert each byte to hex
                }

                return sb.ToString();
            }
        }
    }
}