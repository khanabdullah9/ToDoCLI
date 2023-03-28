using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoCLI
{
    internal class App
    {
        public void Run() 
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Enter password: ");
            var input = Console.ReadLine();
            if (input.Equals("*******"))
            {
                while (!input.ToLower().Equals("exit()"))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Type 1 to show all tasks.");
                    Console.WriteLine("Type 2 to create new task.");
                    Console.WriteLine("Type 3 to delete task.");
                    Console.WriteLine("Type 'exit()' to quit or close the application");
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            Handler.One();
                            break;
                        case "2":
                            Console.Write("Enter task name: ");
                            var taskName = Console.ReadLine();
                            Handler.Two(taskName);
                            break;
                        case "3":
                            Console.Write("Enter ID: ");
                            var ID1 = Console.ReadLine();
                            Handler.Three(ID1);
                            break;
                        default:
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Your input = {input}");
                }
                Console.WriteLine("Have a nice day!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unauthorized intrusion detected!");
            }
        }
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
