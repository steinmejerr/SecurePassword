using System;

namespace SecurePassword.Views
{
    /// <summary>
    /// My View Class
    /// </summary>
    public class UserView
    {
        // Shows what options i have to choose between
        public void ShowMenu()
        {
            Console.WriteLine("===== Secure Login System =====");
            Console.WriteLine("1. Opret");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Forlad");
            Console.Write("Vælg en mulighed: ");
        }

        // Gets the email from the user input
        public string GetEmail()
        {
            Console.Write("Email: ");
            return Console.ReadLine();
        }

        // Gets the password from the user input
        public string GetPassword()
        {
            Console.Write("Adgangskode: ");
            return Console.ReadLine();
        }

        // Shows if the action was a success
        public void ShowSuccess(string message)
        {
            Console.WriteLine($"[✓] {message}");
        }

        // Shows if the action as a error
        public void ShowError(string message)
        {
            Console.WriteLine($"[!] {message}");
        }
    }
}
