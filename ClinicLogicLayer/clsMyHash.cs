using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClinicLogicLayer
{
    public class clsMyHash
    {
        public static string GetSha256Hash(string input)
        {
            // 1. Convert string to bytes
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            // 2. Compute the hash
            byte[] hashBytes;
            using (var sha256 = SHA256.Create())
            {
                hashBytes = sha256.ComputeHash(inputBytes);
            }

            // 3. Convert to Hex string manually (Replaces Convert.ToHexString)
            StringBuilder sb = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2")); // "x2" formats as lowercase hex (e.g., 'a' -> "0a")
            }

            return sb.ToString();

        }
    }
}
