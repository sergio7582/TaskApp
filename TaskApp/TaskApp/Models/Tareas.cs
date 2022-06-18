using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TaskApp.Models
{
    public class Tareas
    {
        [PrimaryKey,AutoIncrement]
        public int IdTarea { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }        
        [MaxLength(15)]
        public string Date { get; set; }

        [MaxLength(10)]
        public string Hour { get; set; }
        public bool IsComplete { get; set; }

    }
}
