using SQLite;

namespace MauiAppSandbox.Services
{
   
    public class CategoryService
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        public SQLiteAsyncConnection conn;

        async Task Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteAsyncConnection(_dbPath);

            await conn.CreateTableAsync<Category>();
        }

        public CategoryService(string dbPath)
        {
            _dbPath = dbPath;
        }


        public async Task<List<Category>> GetAllCategories()
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

        public async Task InsertCategory(Category category)
        {
            int result = 0;
            try
            {
                await Init();

                if (string.IsNullOrEmpty(category.CategoryName))
                    throw new Exception("Valid name required");

                result = await conn.InsertAsync(category);

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, category.CategoryName);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", category.CategoryName, ex.Message);
            }

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
