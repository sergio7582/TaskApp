using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskApp.Models
{
    public class Categorys
    {
        [PrimaryKey, AutoIncrement]
        public int IdCategory { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}
