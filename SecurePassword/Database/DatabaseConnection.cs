using MySql.Data.MySqlClient;
using SecurePassword.Models;
using System;

namespace SecurePassword.Database
{
    /// <summary>
    /// Handles database operations related to user management.
    /// </summary>
    public class DatabaseConnection
    {
        // Connection string to connect to the MySQL database.
        private readonly string connectionString = "server=localhost;user=root;database=secure_password_db;port=3306;password=";

        /// <summary>
        /// Inserts a new user into the database with email, password hash, and salt.
        /// </summary>
        /// <param name="user">The user object containing email, hash, and salt.</param>
        public void InsertUser(User user)
        {
            // Create and open a new database connection.
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // SQL statement to insert a new user.
                string sql = "INSERT INTO users (email, password_hash, salt) VALUES (@Email, @Hash, @Salt)";

                // Create a command and add parameters to prevent SQL injection.
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Hash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Salt", user.Salt);

                // Execute the command to insert the user.
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Retrieves a user from the database by their email.
        /// </summary>
        /// <param name="email">The email of the user to retrieve.</param>
        /// <returns>The matching User object or null if not found.</returns>
        public User GetUserByEmail(string email)
        {
            // Create and open a new database connection.
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // SQL statement to select a user by email.
                string sql = "SELECT * FROM users WHERE email = @Email";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                // Execute the query and read the result.
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Map database fields to a new User object.
                        return new User
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Email = reader["email"].ToString(),
                            PasswordHash = reader["password_hash"].ToString(),
                            Salt = reader["salt"].ToString()
                        };
                    }
                }
            }

            // Return null if no matching user is found.
            return null;
        }

        /// <summary>
        /// Checks whether a user with the given email exists in the database.
        /// </summary>
        /// <param name="email">The email to check.</param>
        /// <returns>True if the user exists, otherwise false.</returns>
        public bool UserExists(string email)
        {
            // Reuse GetUserByEmail to determine existence.
            return GetUserByEmail(email) != null;
        }
    }
}
