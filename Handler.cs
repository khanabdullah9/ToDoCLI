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
                App.Debugger("exception","[ERROR]","Handler","One",ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static void Two(string TaskName, string TaskCategory) 
        {
            try 
            {
                App.Debugger("test","[TEST]","Handler","Two",$"TaskName: {TaskName} and TaskCategory: {TaskCategory}");
                ToDoCLI.CreateNewTask(TaskName, TaskCategory);
            }
            catch (Exception ex) 
            {
                App.Debugger("exception", "[ERROR]", "Handler", "Two", ex.Message);
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
                App.Debugger("exception", "[ERROR]", "Handler", "Three", ex.Message);
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
                App.Debugger("exception", "[ERROR]", "Handler", "Four", ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
