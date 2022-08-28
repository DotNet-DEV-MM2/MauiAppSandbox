using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppSandbox.Interfaces
{
    public interface IAppUserRepository
    {
        Task SaveAsync(AppUser user);
        Task<List<AppUser>> GetAll();
        Task<AppUser> GetById(int id);
        Task DeleteAsync(int id);
        Task DeleteAll();

    }
}
