using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoCLI
{
    internal class App
    {
        public static void Debugger(string type, string description, string class_name,string method, string message)
        {
            switch (type.ToLower())
            {
                case "success":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[{description}] ({class_name}.{method}) : {message}");
                    break;
                case "warning":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[{description}] ({class_name}.{method}) : {message}");
                    break;
                case "exception":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[{description}] ({class_name}.{method}) : {message}");
                    break;
                case "test":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"[{description}] ({class_name}.{method}) : {message}");
                    break;
                default:
                    Console.WriteLine($"[{description}] ({class_name}.{method}) : {message}");
                    break;
            }
        }
    }
}
