using MauiAppSandbox.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppSandbox.Repositories
{

    public class CategoryRepository : ICategoryRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        public SQLiteAsyncConnection conn;

        public CategoryRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        async Task Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteAsyncConnection(_dbPath);

            await conn.CreateTableAsync<Category>();
        }

        public async Task SaveAsync(Category category)
        {
            int result = 0;
            try
            {
                await Init();

                if (category.Id != 0)
                {
                    result = await conn.UpdateAsync(category);
                    StatusMessage = string.Format("{0} record(s) was updated (Name: {1})", result, category.CategoryName);
                }
                else
                {
                    result = await conn.InsertAsync(category);
                    StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, category.CategoryName);
                }

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", category.CategoryName, ex.Message);
            }

        }


        public async Task<List<Category>> GetAll()
        {
            try
            {
                await Init();
                var tempList = await conn.Table<Category>().ToListAsync();

                Debug.WriteLine("CategoryService:GetAllCategories, tempList count " + tempList.Count);
                return tempList;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Category>();
        }


        public async Task<Category> GetById(int id)
        {
            await Init();
            return await conn.Table<Category>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await Init();
            await conn.DeleteAsync(id);
        }

        public async Task DeleteAll()
        {
            try
            {
                await Init();
                var result = await conn.Table<Category>().ToListAsync();
                foreach (var item in result)
                {
                    await conn.DeleteAsync(item);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

        }
    }
}