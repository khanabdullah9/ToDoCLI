using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoCLI
{
    internal class Handler
    {
        public static void One()
        {
            try
            {
                ToDoCLI.SelectAll();
            }
            catch (Exception ex)
            {
                App.Debugger("exception", "ERROR", "Handler", "One", ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static void Two(string TaskName)
        {
            try
            {
                ToDoCLI.CreateNewTask(TaskName);
            }
            catch (Exception ex)
            {
                App.Debugger("exception", "ERROR", "Handler", "Two", ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static void Three(string ID)
        {
            try
            {
                ToDoCLI.DeleteTask(ID);
            }
            catch (Exception ex)
            {
                App.Debugger("exception", "ERROR", "Handler", "Three", ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
