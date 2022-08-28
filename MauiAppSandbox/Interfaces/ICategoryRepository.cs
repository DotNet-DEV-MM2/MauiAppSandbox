using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppSandbox.Interfaces
{
    public interface ICategoryRepository
    {
        Task SaveAsync(Category category);
        Task<List<Category>> GetAll();
        Task<Category> GetById(int id);
        Task DeleteAsync(int id);
        Task DeleteAll();
    }
}
