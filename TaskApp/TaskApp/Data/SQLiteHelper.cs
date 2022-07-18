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
            db.CreateTableAsync<Categorys>().Wait();
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
        public Task<List<Tareas>> GetTasksToCompleteOrNo(bool IsComplete,string resultDate,int category)
        {
            return db.Table<Tareas>().Where
                (a => a.IsComplete == IsComplete && 
                Convert.ToDateTime(a.Date) >= Convert.ToDateTime(resultDate) 
                && a.idCategory == category).ToListAsync();
        }
        public Task<Tareas> GetTasksById(int IdTask)
        {
            return db.Table<Tareas>().Where(a => a.IdTarea == IdTask).FirstOrDefaultAsync();
        }
        public Task<List<Tareas>> GetTaskByDay(string resultDate)
        {            
            return db.Table<Tareas>().Where(a => a.IsComplete == false && a.Date == resultDate).ToListAsync();
        }
        public Task<List<Tareas>> GetTaskByTomorrow(string resultDate)
        {
            return db.Table<Tareas>().Where(a => a.IsComplete == false && Convert.ToDateTime(a.Date) >= Convert.ToDateTime(resultDate)).ToListAsync();
        }

        public Task<List<Tareas>> GetTasksByIdCategory(int idCategory)
        {
            return db.Table<Tareas>().Where(a => a.idCategory == idCategory).ToListAsync();
        }




        public Task<List<Categorys>> GetCategorysAsync()
        {
            return db.Table<Categorys>().ToListAsync();
        }
        public Task<Categorys> GetCategorysById(int IdCategory)
        {
            return db.Table<Categorys>().Where(a => a.IdCategory == IdCategory).FirstOrDefaultAsync();
        }
        public Task<int> SaveCategoryAsync(Categorys categorys)
        {
            if (categorys.IdCategory == 0)
            {
                return db.InsertAsync(categorys);
            }
            else
            {
                return db.UpdateAsync(categorys);
            }
        }

        public async Task<bool> DeleteCategory(int idCategory)
        {
            db.Table<Categorys>().DeleteAsync(a => a.IdCategory == idCategory).Wait();
            return true;
        }



    }
}
