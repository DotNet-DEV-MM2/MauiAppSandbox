using SQLite;

namespace MauiAppSandbox.Models
{
    [Table("ClosetItems")]
    public class ClosetItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string PictureUri { get; set; }

        [MaxLength(250), Unique]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string Season { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
    

}
