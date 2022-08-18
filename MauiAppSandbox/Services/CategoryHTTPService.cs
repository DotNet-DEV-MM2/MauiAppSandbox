using MauiAppSandbox.Models;

namespace MauiAppSandbox.Services
{
    public class CategoryHTTPService
    {
        HttpClient httpClient;

        public CategoryHTTPService()
        {
            this.httpClient = new HttpClient();
        }

        List<Category> categoryList;

        public async Task<List<Category>> GetCategories()
        {
            if (categoryList?.Count > 0)
                return categoryList;

            // Online
            /*var response = await httpClient.GetAsync("https://www.montemagno.com/monkeys.json");
            if (response.IsSuccessStatusCode)
            {
                closetItemList = await response.Content.ReadFromJsonAsync<List<ClosetItem>>();
            }*/

            // Offline
            using var stream = await FileSystem.OpenAppPackageFileAsync("categorydata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            categoryList = JsonSerializer.Deserialize<List<Category>>(contents);

            return categoryList;
        }
    }
}
