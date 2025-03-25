using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace SecurePassword.Utils
{
    /// <summary>
    /// Provides methods to securely hash and verify passwords using PBKDF2.
    /// </summary>
    public static class PasswordHasher
    {
        /// <summary>
        /// Hashes a plaintext password using PBKDF2 and generates a unique salt.
        /// </summary>
        /// <param name="password">The plaintext password to be hashed.</param>
        /// <returns>A tuple containing the base64-encoded hash and salt.</returns>
        public static (string Hash, string Salt) HashPassword(string password)
        {
            // Generate a 16-byte cryptographic salt.
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes); // Fill the array with secure random bytes.
            }

            // Use PBKDF2 to derive a 32-byte key from the password and salt.
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(32); // 32 bytes = 256 bits
                // Return both the hash and the salt encoded in Base64.
                return (Convert.ToBase64String(hash), Convert.ToBase64String(saltBytes));
            }
        }

        /// <summary>
        /// Verifies a plaintext password against the stored hash and salt.
        /// </summary>
        /// <param name="password">The plaintext password to verify.</param>
        /// <param name="storedHash">The previously stored password hash (Base64).</param>
        /// <param name="storedSalt">The previously stored salt (Base64).</param>
        /// <returns>True if the password matches the hash, otherwise false.</returns>
        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            // Decode the stored Base64-encoded salt and hash.
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            // Re-hash the input password using the original salt.
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
            {
                byte[] computedHash = pbkdf2.GetBytes(32);
                // Use structural comparison to safely check byte arrays for equality.
                return StructuralComparisons.StructuralEqualityComparer.Equals(hashBytes, computedHash);
            }
        }
    }
}
