using MySql.Data.MySqlClient;
using SecurePassword.Models;
using SecurePassword.Models;
using System;

namespace SecurePassword.Database
{
    /// <summary>
    /// My connection to the database
    /// </summary>
    public class DatabaseConnection
    {
        private readonly string connectionString = "server=localhost;user=root;database=secure_password_db;port=3306;password=";

        public void InsertUser(User user)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO users (email, password_hash, salt) VALUES (@Email, @Hash, @Salt)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Hash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Salt", user.Salt);
                cmd.ExecuteNonQuery();
            }
        }

        public User GetUserByEmail(string email)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM users WHERE email = @Email";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
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
            return null;
        }

        public bool UserExists(string email)
        {
            return GetUserByEmail(email) != null;
        }
    }
}
