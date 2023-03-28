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
        private static string _tableName = "Task";
        private static string _procName = "spCreateTask";

        /// <summary>
        /// Select all rows from table
        /// <para></para>
        /// <return>void</return>
        /// </summary>
        public static void SelectAll() 
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
                    using (SqlCommand cmd = new SqlCommand($"select * from {_tableName}", con))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string ID = dr["ID"].ToString();
                                string TaskName = dr["TaskName"].ToString();
                                string CreatedAt = dr["CreatedAt"].ToString();
                                ToDoTable toDoTable = new ToDoTable()
                                {
                                    ID = ID,
                                    TaskName = TaskName,
                                    CreatedAt = CreatedAt,
                                };
                                AllRows.Add(toDoTable);
                            }
                            PrintTable(AllRows);
                        }
                        else 
                        {
                            App.Debugger("warning","TEST","TodoCLI","SelectAsterick","No tasks left");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                App.Debugger("exception", "ERROR", "ToDoCLI", "SelectAsterick", ex.Message);
            }
        }

        /// <summary>
        /// Print all the rows
        /// <para value="AllRows">List of all the db rows</para>
        /// <return>void</return>
        /// </summary>
        public static void PrintTable(List<ToDoTable> AllRows) 
        {
            /*MAXIMUM LENGTHS OF COLUMNS*/
            int colID = AllRows.ElementAt(0).ID.Length; 
            int TaskName = AllRows.ElementAt(0).TaskName.Length; 
            int CreatedAt = AllRows.ElementAt(0).CreatedAt.Length;
            for (int i=0;i<AllRows.Count;i++) 
            {
                ToDoTable toDoTable = AllRows.ElementAt(i);
                if (colID < toDoTable.ID.Length) { colID = toDoTable.ID.Length; }
                if (TaskName < toDoTable.TaskName.Length) { TaskName = toDoTable.TaskName.Length; }
                if (CreatedAt < toDoTable.CreatedAt.Length) { CreatedAt = toDoTable.CreatedAt.Length; }
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
            Console.Write("Created At");
            for (int ca = 1; ca <= (CreatedAt - "Created At".Length)+3; ++ca)
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
                
                Console.Write(row.CreatedAt);
                for (int ca = 1; ca <= (CreatedAt - row.CreatedAt.Length)+3; ++ca)
                {
                    Console.Write(" ");
                }
                
                
                Console.WriteLine(" ");
                Console.WriteLine(" ");
            }
        }
        /// <summary>
        /// Creates new task in the database
        /// <para value="TaskName">Name of the task</para>
        /// <return>void</return>
        /// </summary>
        /// <param name="TaskName"></param>
        public static void CreateNewTask(string TaskName) 
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
                    using (SqlCommand cmd = new SqlCommand($"EXEC {_procName} '{TaskName}','{CreatedAt.ToString("dd-MM-yy")}'", con))
                    {
                        cmd.ExecuteNonQuery();
                        ToDoCLI.SelectAll();
                    }
                }
            }
            catch (Exception ex) 
            {
                App.Debugger("exception","ERROR","ToDoCLI","CreateNewTask",ex.Message);
            }
        }


        /// <summary>
        /// Delete a tasj
        /// <para value="TaskId">Id of the task</para>
        /// <return>void</return>
        /// </summary>
        /// <param name="TaskId"></param>
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
                        ToDoCLI.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                App.Debugger("exception", "ERROR", "ToDoCLI", "DeleteTask", ex.Message);
            }
        }
    }
}
