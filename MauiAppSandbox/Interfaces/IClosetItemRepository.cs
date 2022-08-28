using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppSandbox.Interfaces
{
    public interface IClosetItemRepository
    {
        Task SaveAsync(ClosetItem closetItem);
        Task<List<ClosetItem>> GetAll();
        Task<ClosetItem> GetById(int id);
        Task DeleteAsync(int id);
        Task DeleteAll();

    }
}
