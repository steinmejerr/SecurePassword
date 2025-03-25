using System;
using SecurePassword.Controllers;

namespace SecurePassword
{
    /// <summary>
    /// Entry point of the SecurePassword application.
    /// Initializes and starts the user controller.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main method that runs when the application starts.
        /// </summary>
        /// <param name="args">Command-line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Create a new instance of the UserController, which handles user input and logic.
            UserController controller = new UserController();

            // Start the main application loop.
            controller.Start();
        }
    }
}
