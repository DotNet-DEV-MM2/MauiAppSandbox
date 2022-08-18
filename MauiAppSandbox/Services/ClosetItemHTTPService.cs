using MauiAppSandbox.Models;

namespace MauiAppSandbox.Services
{
    public class ClosetItemHTTPService
    {
        HttpClient httpClient;

        public ClosetItemHTTPService()
        {
            this.httpClient = new HttpClient();
        }

        List<ClosetItem> closetItemList;

        public async Task<List<ClosetItem>> GetClosetItems()
        {
            if (closetItemList?.Count > 0)
                return closetItemList;

            // Online
            /*var response = await httpClient.GetAsync("https://www.montemagno.com/monkeys.json");
            if (response.IsSuccessStatusCode)
            {
                closetItemList = await response.Content.ReadFromJsonAsync<List<ClosetItem>>();
            }*/

            // Offline
            using var stream = await FileSystem.OpenAppPackageFileAsync("closetitemdata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            closetItemList = JsonSerializer.Deserialize<List<ClosetItem>>(contents);

            return closetItemList;
        }
    }
}
