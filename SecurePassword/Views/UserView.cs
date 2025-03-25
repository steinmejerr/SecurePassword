using System;

namespace SecurePassword.Views
{
    /// <summary>
    /// Responsible for handling user interface interactions in the console.
    /// </summary>
    public class UserView
    {
        /// <summary>
        /// Displays the main menu options to the user.
        /// </summary>
        public void ShowMenu()
        {
            Console.WriteLine("===== Secure Login System =====");
            Console.WriteLine("1. Opret");   // Register
            Console.WriteLine("2. Login");   // Log in
            Console.WriteLine("3. Forlad");  // Exit
            Console.Write("Vælg en mulighed: "); // Prompt to choose an option
        }

        /// <summary>
        /// Prompts the user to enter their email.
        /// </summary>
        /// <returns>The email entered by the user.</returns>
        public string GetEmail()
        {
            Console.Write("Email: ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Prompts the user to enter their password.
        /// </summary>
        /// <returns>The password entered by the user.</returns>
        public string GetPassword()
        {
            Console.Write("Adgangskode: ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Displays a success message to the user.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public void ShowSuccess(string message)
        {
            Console.WriteLine($"[✓] {message}");
        }

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public void ShowError(string message)
        {
            Console.WriteLine($"[!] {message}");
        }
    }
}
