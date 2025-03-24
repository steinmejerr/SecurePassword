using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace SecurePassword.Utils
{
    /// <summary>
    /// My hashing class
    /// </summary>
    public static class PasswordHasher
    {
        public static (string Hash, string Salt) HashPassword(string password)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                return (Convert.ToBase64String(hash), Convert.ToBase64String(saltBytes));
            }
        }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
            {
                byte[] computedHash = pbkdf2.GetBytes(32);
                return StructuralComparisons.StructuralEqualityComparer.Equals(hashBytes, computedHash);
            }
        }
    }
}
