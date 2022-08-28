using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppSandbox.Models
{
    [Table("AppUsers")]
    public class AppUser
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string EmailConfirmed { get; set; }
        public string Role { get; set; }    
        //public ICollection<ClosetItem> ClosetItems { get; set; }

    }
}