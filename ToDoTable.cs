using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoCLI
{
    internal class ToDoTable
    {
        public string ID { get; set; }
        public string TaskName { get; set; }
        public string TaskCategory { get; set; }
        public string FinishedAt { get; set; }
        public string CreatedAt { get; set; }
        public string Completed { get; set; }
    }
}
