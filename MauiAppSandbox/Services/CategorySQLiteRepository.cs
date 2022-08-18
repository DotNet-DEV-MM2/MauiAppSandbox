using SQLite;

namespace MauiAppSandbox.Services
{
    public class CategorySQLiteRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        private SQLiteAsyncConnection conn;

        private async Task Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteAsyncConnection(_dbPath);
            await conn.CreateTableAsync<Category>();
        }
        public CategorySQLiteRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                await Init();
                return await conn.Table<Category>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Category>();
        }

        public async Task InsertCategory(Category category)
        {
            int result = 0;
            try
            {
                await Init();

                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(category.CategoryName))
                    throw new Exception("Valid name required");

                result = await conn.InsertAsync(new Category { CategoryName = category.CategoryName });

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, category.CategoryName);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", category.CategoryName, ex.Message);
            }

        }

    }
}
