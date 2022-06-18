using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TaskApp.Models;

namespace TaskApp.Data
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Tareas>().Wait();
        }

        public Task<int> SaveTaskAsync(Tareas task)
        {
            if(task.IdTarea == 0)
            {
                return db.InsertAsync(task);
            }
            else
            {
                return db.UpdateAsync(task);
            }
        }        
        public Task<List<Tareas>> GetTasksToCompleteOrNo(bool IsComplete,string resultDate)
        {
            return db.Table<Tareas>().Where(a => a.IsComplete == IsComplete && a.Date != resultDate).ToListAsync();
        }

        public Task<List<Tareas>> GetTasksUrgents(string date)
        {
            return db.Table<Tareas>().Where(a => a.IsComplete == false && a.Date == date).ToListAsync();
        }
        public Task<Tareas> GetTasksById(int IdTask)
        {
            return db.Table<Tareas>().Where(a => a.IdTarea == IdTask).FirstOrDefaultAsync();
        }
    }
}
