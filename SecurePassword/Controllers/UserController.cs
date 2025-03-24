using SecurePassword.Models;
using SecurePassword.Views;
using SecurePassword.Database;
using SecurePassword.Models;
using SecurePassword.Utils;
using SecurePassword.Views;
using System;

namespace SecurePassword.Controllers
{
    public class UserController
    {
        private readonly UserView view = new UserView();
        private readonly DatabaseConnection db = new DatabaseConnection();

        public void Start()
        {
            while (true)
            {
                view.ShowMenu();
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Register();
                        break;
                    case "2":
                        Login();
                        break;
                    case "3":
                        return;
                    default:
                        view.ShowError("Invalid option.");
                        break;
                }
                Console.WriteLine();
            }
        }

        private void Register()
        {
            string email = view.GetEmail();
            string password = view.GetPassword();

            if (db.UserExists(email))
            {
                view.ShowError("User already exists.");
                return;
            }

            var (hash, salt) = PasswordHasher.HashPassword(password);
            User user = new User { Email = email, PasswordHash = hash, Salt = salt };
            db.InsertUser(user);

            view.ShowSuccess("User registered successfully.");
        }

        private void Login()
        {
            string email = view.GetEmail();
            string password = view.GetPassword();

            User user = db.GetUserByEmail(email);
            if (user == null)
            {
                view.ShowError("User not found.");
                return;
            }

            bool isValid = PasswordHasher.VerifyPassword(password, user.PasswordHash, user.Salt);
            if (isValid)
                view.ShowSuccess("Login successful!");
            else
                view.ShowError("Invalid credentials.");
        }
    }
}
