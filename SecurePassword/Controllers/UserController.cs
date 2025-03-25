using SecurePassword.Models;
using SecurePassword.Views;
using SecurePassword.Database;
using SecurePassword.Utils;
using System;

namespace SecurePassword.Controllers
{
    /// <summary>
    /// Handles user-related operations such as registration and login.
    /// Connects the view, database, and utility classes together.
    /// </summary>
    public class UserController
    {
        // Instance of the view to handle user input/output.
        private readonly UserView view = new UserView();

        // Instance of the database connection for data operations.
        private readonly DatabaseConnection db = new DatabaseConnection();

        /// <summary>
        /// Starts the application loop to display menu and handle user choices.
        /// </summary>
        public void Start()
        {
            while (true)
            {
                // Display the main menu to the user.
                view.ShowMenu();
                string input = Console.ReadLine();

                // Handle user's menu selection.
                switch (input)
                {
                    case "1":
                        Register();
                        break;
                    case "2":
                        Login();
                        break;
                    case "3":
                        // Exit the application loop.
                        return;
                    default:
                        // Show error message for invalid input.
                        view.ShowError("Invalid option.");
                        break;
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Handles the user registration process, including validation and saving to the database.
        /// </summary>
        private void Register()
        {
            // Get email and password input from the user.
            string email = view.GetEmail();
            string password = view.GetPassword();

            // Check if the user already exists in the database.
            if (db.UserExists(email))
            {
                view.ShowError("User already exists.");
                return;
            }

            // Hash the password and generate a salt.
            var (hash, salt) = PasswordHasher.HashPassword(password);

            // Create a new User object with the provided email and hashed password.
            User user = new User { Email = email, PasswordHash = hash, Salt = salt };

            // Insert the new user into the database.
            db.InsertUser(user);

            // Notify the user of successful registration.
            view.ShowSuccess("User registered successfully.");
        }

        /// <summary>
        /// Handles the user login process, including password verification.
        /// </summary>
        private void Login()
        {
            // Get email and password input from the user.
            string email = view.GetEmail();
            string password = view.GetPassword();

            // Retrieve the user from the database by email.
            User user = db.GetUserByEmail(email);
            if (user == null)
            {
                // If no user is found, display an error.
                view.ShowError("User not found.");
                return;
            }

            // Verify the entered password against the stored hash and salt.
            bool isValid = PasswordHasher.VerifyPassword(password, user.PasswordHash, user.Salt);

            // Show appropriate message based on the result.
            if (isValid)
                view.ShowSuccess("Login successful!");
            else
                view.ShowError("Invalid credentials.");
        }
    }
}
