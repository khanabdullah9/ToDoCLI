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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            PrintTable(AllRows);
        }

        /*
         * Print all the rows
         * params -> List of of all the db rows
         */
        public static void PrintTable(List<ToDoTable> AllRows) 
        {
            /*MAXIMUM LENGTHS OF COLUMNS*/
            int colID = 0, TaskName = 0, TaskCategory= 0, CreatedAt = 0, FinishedAt = 0, Completed = 0;
            for (int i=0;i<AllRows.Count;++i) 
            {
                ToDoTable toDoTable = AllRows[i];
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
            for (int id=1;id<=colID-"ID".Length;++id) 
            {
                Console.Write(" ");
            }
            Console.Write("Task Name");
            for (int tn = 1; tn <= colID - "Task Name".Length; ++tn)
            {
                Console.Write(" ");
            }
            Console.Write("Task Category");
            for (int tc = 1; tc <= colID - "Task Category".Length; ++tc)
            {
                Console.Write(" ");
            }
            Console.Write("Created At");
            for (int ca = 1; ca <= colID - "Created At".Length; ++ca)
            {
                Console.Write(" ");
            }
            Console.Write("Finished At");
            for (int fa = 1; fa <= colID - "Finished At".Length; ++fa)
            {
                Console.Write(" ");
            }
            Console.Write("Completed");
            for (int co = 1; co <= colID - "Completed".Length; ++co)
            {
                Console.Write(" ");
            }
            Console.WriteLine(" ");
            Console.WriteLine(" ");//Move to next line
            Console.ForegroundColor = ConsoleColor.White;
            //Print the content row by row
            foreach (var row in AllRows) 
            {
                Console.WriteLine(row.ID);
                for (int id = 1; id <= colID - "ID".Length; ++id)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(row.TaskName);
                for (int tn = 1; tn <= colID - "Task Name".Length; ++tn)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(row.TaskCategory);
                for (int tc = 1; tc <= colID - "Task Category".Length; ++tc)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(row.CreatedAt);
                for (int ca = 1; ca <= colID - "Created At".Length; ++ca)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(row.FinishedAt);
                for (int fa = 1; fa <= colID - "Finished At".Length; ++fa)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(row.Completed);
                for (int co = 1; co <= colID - "Completed".Length; ++co)
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
                    string ID = Guid.NewGuid().ToString();
                    DateTime CreatedAt = DateTime.Now;
                    DateTime FinishedAt = new DateTime(0,0,0,0,0,0);
                    using (SqlCommand cmd = new SqlCommand($"insert into {_tableName} values ({ID},{TaskName},{TaskCategory},{CreatedAt},{FinishedAt},0)",con)) 
                    {
                        cmd.ExecuteNonQuery();
                        App.Debugger("success","SUCCESS","ToDoCLI","CreateNewTask",$"New task '{TaskName}' created successfully!");
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
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
                        App.Debugger("success", "SUCCESS", "ToDoCLI", "DeleteTask", $"TaskId: {TaskId} has been deleted successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                        App.Debugger("success", "SUCCESS", "ToDoCLI", "SetTaskCompleted", $"TaskId: {TaskId} has been updated successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
