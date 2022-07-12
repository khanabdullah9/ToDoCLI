using System;

namespace ToDoCLI 
{
    public class Program 
    {
        public static void Main() 
        {
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Enter password: ");
            var input = Console.ReadLine();
            if (input.Equals("pa55word"))
            {
                while (!input.ToLower().Equals("exit()"))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Type 1 to show all tasks.");
                    Console.WriteLine("Type 2 to create new task.");
                    Console.WriteLine("Type 3 to delete task.");
                    Console.WriteLine("Type 4 to set task completed.");
                    Console.WriteLine("Type 5 to view all the task categories");
                    Console.WriteLine("Type 6 to view all pending tasks");
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
                            Console.WriteLine("Enter task categry: ");
                            var taskCategory = Console.ReadLine();
                            Handler.Two(taskName,taskCategory);
                            break;
                        case "3":
                            Console.Write("Enter ID: ");
                            var ID1 = Console.ReadLine();
                            Handler.Three(ID1);
                            break;
                        case "4":
                            Console.Write("Enter ID: ");
                            var ID2 = Console.ReadLine();
                            Handler.Four(ID2);
                            break;
                        case "5":
                            Handler.Five();
                            break;
                        case "6":
                            Handler.Six();
                            break;
                        default:
                            //Console.ForegroundColor = ConsoleColor.Yellow;
                            //Console.WriteLine($"{input} is not a valid input");
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
            //Console.ReadKey();
        }
    }
}