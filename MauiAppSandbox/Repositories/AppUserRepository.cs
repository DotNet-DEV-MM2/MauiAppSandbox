using MauiAppSandbox.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppSandbox.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        public SQLiteAsyncConnection conn;

        public AppUserRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        async Task Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteAsyncConnection(_dbPath);

            await conn.CreateTableAsync<AppUser>();
        }

        public async Task SaveAsync(AppUser appuser)
        {
            int result = 0;
            try
            {
                await Init();

                if (appuser.Id != 0)
                {
                    result = await conn.UpdateAsync(appuser);
                    StatusMessage = string.Format("{0} record(s) was updated (Name: {1})", result, appuser.UserName);
                }
                else
                {
                    result = await conn.InsertAsync(appuser);
                    StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, appuser.UserName);
                }

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", appuser.UserName, ex.Message);
            }

        }

        public async Task<List<AppUser>> GetAll()
        {
            try
            {
                await Init();
                var tempList = await conn.Table<AppUser>().ToListAsync();

                Debug.WriteLine("CategoryService:GetAllCategories, tempList count " + tempList.Count);
                return tempList;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<AppUser>();
        }

        public async Task<AppUser> GetById(int id)
        {
            await Init();
            return await conn.Table<AppUser>().Where(x => x.Id == id).FirstOrDefaultAsync();
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
                var result = await conn.Table<AppUser>().ToListAsync();
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