using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppSandbox.Services
{
    public class ClosetItemSQLiteRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        // TODO: Add variable for the SQLite connection
        private SQLiteAsyncConnection conn;

        private async Task Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteAsyncConnection(_dbPath);
            await conn.CreateTableAsync<ClosetItem>();
        }
        public ClosetItemSQLiteRepository(string dbPath)
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

                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(closetitem.Name))
                    throw new Exception("Valid name required");

                result = await conn.InsertAsync(new ClosetItem { Name = closetitem.Name });

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, closetitem.Name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", closetitem.Name, ex.Message);
            }

        }

    }
}
