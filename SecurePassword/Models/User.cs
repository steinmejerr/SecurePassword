using System;

namespace SecurePassword.Models
{
    /// <summary>
    /// Represents a user entity with credentials for authentication.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique identifier for the user (Primary Key in the database).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user's email address used for login.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The hashed version of the user's password.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// The cryptographic salt used when hashing the password.
        /// </summary>
        public string Salt { get; set; }
    }
}
