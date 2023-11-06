using System.Security.Cryptography;
using System.Text;

namespace Mentorias.Utils
{
    public  class HashUtility
    {
        public static string GenerateSHA256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Converte o byte para formato hexadecimal
                }

                return builder.ToString();
            }
        }
    }
}
