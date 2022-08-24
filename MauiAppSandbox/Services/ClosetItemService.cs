using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppSandbox.Services
{
    public class ClosetItemService
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        private SQLiteAsyncConnection conn;

        private async Task Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteAsyncConnection(_dbPath);
            await conn.CreateTableAsync<ClosetItem>();
        }
        public ClosetItemService(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task<List<ClosetItem>> GetAllClosetItems()
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

        public async Task InsertClosetItem(ClosetItem closetitem)
        {
            int result = 0;
            try
            {
                await Init();

                if (string.IsNullOrEmpty(closetitem.Name))
                    throw new Exception("Valid name required");

                result = await conn.InsertAsync(closetitem);

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, closetitem.Name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", closetitem.Name, ex.Message);
            }

        }

        public async Task DeleteAll()
        {
            await conn.Table<ClosetItem>().DeleteAsync();
        }

    }
}
