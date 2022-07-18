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
        [MaxLength(300)]
        public string Tarea { get; set; }
       
        public int idCategory { get; set; }
        [MaxLength(10)]
        public string Date { get; set; }

              
        public bool IsComplete { get; set; }

    }
}
