using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ToDoCLI
{
    internal class ToDoCLI
    {
        private static string _strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        private static string _tableName = "ToDo";

        /*
         * Select all rows from table
         * returns -> List object of ToDoTable class that represents the database table
         */
        public static void SelectAsterick() 
        {
            List<ToDoTable> AllRows = new List<ToDoTable>();
            try
            {
                using (SqlConnection con = new SqlConnection(_strcon)) 
                {
                    if (con.State == ConnectionState.Closed) 
                    {
                        con.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand($"select * from {_tableName}",con)) 
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string ID = dr["ID"].ToString();
                                string TaskName = dr["TaskName"].ToString();
                                string TaskCategory = dr["TaskCategory"].ToString();
                                string CreatedAt = dr["CreatedAt"].ToString();
                                string FinishedAt = dr["FinishedAt"].ToString();
                                string Completed = dr["Completed"].ToString();
                                ToDoTable toDoTable = new ToDoTable()
                                {
                                    ID = ID,
                                    TaskName = TaskName,
                                    TaskCategory = TaskCategory,
                                    CreatedAt = CreatedAt,
                                    FinishedAt = FinishedAt,
                                    Completed = Completed,
                                };
                                AllRows.Add(toDoTable);
                            }
                        }
                    }
                }
                PrintTable(AllRows);
            }
            catch (Exception ex)
            {
                App.Debugger("exception", "ERROR", "ToDoCLI", "SelectAsterick", ex.Message);
            }
        }

        /*
         * Print all the rows
         * params -> List of of all the db rows
         */
        public static void PrintTable(List<ToDoTable> AllRows) 
        {
            /*MAXIMUM LENGTHS OF COLUMNS*/
            int colID = AllRows.ElementAt(0).ID.Length, TaskName = AllRows.ElementAt(0).TaskName.Length, TaskCategory= AllRows.ElementAt(0).TaskCategory.Length, CreatedAt = AllRows.ElementAt(0).CreatedAt.Length, FinishedAt = AllRows.ElementAt(0).FinishedAt.Length, Completed = AllRows.ElementAt(0).Completed.Length;
            for (int i=0;i<AllRows.Count;++i) 
            {
                ToDoTable toDoTable = AllRows.ElementAt(i);
                if (colID < toDoTable.ID.Length) { colID = toDoTable.ID.Length; }
                if (TaskName < toDoTable.TaskName.Length) { TaskName = toDoTable.TaskName.Length; }
                if (TaskCategory < toDoTable.TaskCategory.Length) { TaskCategory = toDoTable.TaskCategory.Length; }
                if (CreatedAt < toDoTable.CreatedAt.Length) { CreatedAt = toDoTable.CreatedAt.Length; }
                if (FinishedAt < toDoTable.FinishedAt.Length) { FinishedAt = toDoTable.FinishedAt.Length; }
                if (Completed < toDoTable.Completed.Length) { Completed = toDoTable.Completed.Length; }
            }
            //Print the header
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("ID");
            for (int id=1;id<=(colID - "ID".Length)+3;++id) 
            {
                Console.Write(" ");
            }
            Console.Write("Task Name");
            for (int tn = 1; tn <= (TaskName - "Task Name".Length)+3; ++tn)
            {
                Console.Write(" ");
            }
            Console.Write("Task Category");
            for (int tc = 1; tc <= (TaskCategory - "Task Category".Length)+3; ++tc)
            {
                Console.Write(" ");
            }
            Console.Write("Created At");
            for (int ca = 1; ca <= (CreatedAt - "Created At".Length)+3; ++ca)
            {
                Console.Write(" ");
            }
            Console.Write("Finished At");
            for (int fa = 1; fa <= (FinishedAt - "Finished At".Length)+3; ++fa)
            {
                Console.Write(" ");
            }
            Console.Write("Completed");
            for (int co = 1; co <= (Completed - "Completed".Length)+3; ++co)
            {
                Console.Write(" ");
            }
            Console.WriteLine(" ");
            Console.WriteLine(" ");//Move to next line
            Console.ForegroundColor = ConsoleColor.Green;
            //Print the content row by row
            /*(Length of the longest string - the content string length) + 3 for extra spacing*/
            foreach (var row in AllRows) 
            {
                Console.Write(row.ID);
                for (int id = 1; id <= (colID-row.ID.Length) + 3; ++id)
                {
                    Console.Write(" ");
                }
                Console.Write(row.TaskName);
                for (int tn = 1; tn <= (TaskName - row.TaskName.Length)+3; ++tn)
                {
                    Console.Write(" ");
                }
                Console.Write(row.TaskCategory);
                for (int tc = 1; tc <= (TaskCategory - row.TaskCategory.Length)+3; ++tc)
                {
                    Console.Write(" ");
                }
                Console.Write(row.CreatedAt);
                for (int ca = 1; ca <= (CreatedAt - row.CreatedAt.Length)+3; ++ca)
                {
                    Console.Write(" ");
                }
                Console.Write(row.FinishedAt);
                for (int fa = 1; fa <= (FinishedAt - row.FinishedAt.Length)+3; ++fa)
                {
                    Console.Write(" ");
                }
                Console.Write(row.Completed);
                for (int co = 1; co <= (Completed - row.Completed.Length)+3; ++co)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(" ");
                Console.WriteLine(" ");
            }
        }
        /*
         * Creates new task in the database
         * params -> TaskName: Name of the task
         * TaskCategory: Category of the task
         * returns -> void
         */
        public static void CreateNewTask(string TaskName, string TaskCategory) 
        {
            try 
            {
                using (SqlConnection con = new SqlConnection(_strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    DateTime CreatedAt = DateTime.Now;
                    using (SqlCommand cmd = new SqlCommand($"insert into {_tableName} values ('{TaskName}','{TaskCategory}','{CreatedAt.ToString()}','PENDING',0)", con))
                    {
                        cmd.ExecuteNonQuery();
                        ToDoCLI.SelectAsterick();
                        //App.Debugger("success", "SUCCESS", "ToDoCLI", "CreateNewTask", $"New task '{TaskName}' created successfully!");
                    }
                }
            }
            catch (Exception ex) 
            {
                App.Debugger("exception","ERROR","ToDoCLI","CreateNewTask",ex.Message);
            }
        }

        /*
         * Delete existing task
         * params -> TaskId: Id of the task
         * returns -> void
         */
        public static void DeleteTask(string TaskId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string ID = Guid.NewGuid().ToString();
                    using (SqlCommand cmd = new SqlCommand($"delete from {_tableName} where ID={TaskId}", con))
                    {
                        cmd.ExecuteNonQuery();
                        ToDoCLI.SelectAsterick();
                        //App.Debugger("success", "SUCCESS", "ToDoCLI", "DeleteTask", $"TaskId: {TaskId} has been deleted successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                App.Debugger("exception", "ERROR", "ToDoCLI", "DeleteTask", ex.Message);
            }
        }

        /*
         * Set the completed value true in the db
         * params -> TaskId: Id of the task
         * returns -> void
         */
        public static void SetTaskCompleted(string TaskId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string ID = Guid.NewGuid().ToString();
                    DateTime currentTime = DateTime.Now;
                    using (SqlCommand cmd = new SqlCommand($"update {_tableName} set completed=1, FinishedAt='{currentTime}' where Id='{TaskId}'", con))
                    {
                        cmd.ExecuteNonQuery();
                        ToDoCLI.SelectAsterick();
                        //App.Debugger("success", "SUCCESS", "ToDoCLI", "SetTaskCompleted", $"TaskId: {TaskId} has been updated successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                App.Debugger("exception", "ERROR", "ToDoCLI", "SetTaskCompleted", ex.Message);
            }
        }

        /*Show all task categories from the database*/
        public static void ShowAllCategories() 
        {
            try 
            {
                using (SqlConnection con = new SqlConnection(_strcon))
                {
                    if (con.State == ConnectionState.Closed) 
                    {
                        con.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand($"select TaskCategory from {_tableName}",con))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Task Category/ies");
                        if (dr.HasRows) 
                        {
                            while (dr.Read()) 
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(dr["TaskCategory"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                App.Debugger("exception","ERROR","ToDoCLI","ShowAllCategories",ex.Message);
            }
        }

        /*Show all task categories from the database*/
        public static void ShowAllPendingTasks()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand($"select TaskName from {_tableName} where FinishedAt='PENDING' ", con))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Pending task/s");
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(dr["TaskName"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                App.Debugger("exception", "ERROR", "ToDoCLI", "ShowAllPendingTasks", ex.Message);
            }
        }
    }
}
