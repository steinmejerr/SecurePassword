using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurePassword.Controllers;

namespace SecurePassword
{
    /// <summary>
    /// My main class
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            UserController controller = new UserController(); // Make a new instance of the controller
            controller.Start(); // Start the program
        }
    }
}
