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
                ToDoCLI.SelectAsterick();
            }
            catch (Exception ex)
            {
                App.Debugger("exception", "ERROR", "Handler", "One", ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static void Two(string TaskName, string TaskCategory)
        {
            try
            {
                ToDoCLI.CreateNewTask(TaskName, TaskCategory);
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

        public static void Four(string ID)
        {
            try
            {
                ToDoCLI.SetTaskCompleted(ID);
            }
            catch (Exception ex)
            {
                App.Debugger("exception", "ERROR", "Handler", "Four", ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static void Five() 
        {
            try
            {
                ToDoCLI.ShowAllCategories();
            }
            catch (Exception ex) 
            {
                App.Debugger("exception", "ERROR", "Handler", "Five", ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static void Six()
        {
            try
            {
                ToDoCLI.ShowAllPendingTasks();
            }
            catch (Exception ex)
            {
                App.Debugger("exception", "ERROR", "Handler", "Six", ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
