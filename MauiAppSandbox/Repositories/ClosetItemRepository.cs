using MauiAppSandbox.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppSandbox.Repositories
{
    public class ClosetItemRepository : IClosetItemRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        private SQLiteAsyncConnection conn;

        public ClosetItemRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        private async Task Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteAsyncConnection(_dbPath);
            await conn.CreateTableAsync<ClosetItem>();
        }

        public async Task SaveAsync(ClosetItem closetitem)
        {
            int result = 0;
            try
            {
                await Init();

                if (closetitem.Id != 0)
                {
                    result = await conn.UpdateAsync(closetitem);
                    StatusMessage = string.Format("{0} record(s) was updated (Name: {1})", result, closetitem.Name);
                }
                else
                {
                    result = await conn.InsertAsync(closetitem);
                    StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, closetitem.Name);
                }

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", closetitem.Name, ex.Message);
            }

        }

        public async Task<List<ClosetItem>> GetAll()
        {
            try
            {
                await Init();
                return await conn.Table<ClosetItem>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<ClosetItem>();
        }

        public async Task<ClosetItem> GetById(int id)
        {
            await Init();
            return await conn.Table<ClosetItem>().Where(x => x.Id == id).FirstOrDefaultAsync();
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
                var result = await conn.Table<ClosetItem>().ToListAsync();
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