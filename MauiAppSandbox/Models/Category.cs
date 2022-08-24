﻿using SQLite;

namespace MauiAppSandbox.Models
{
    [Table ("Categories")]
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Color { get; set; }
        public string CategoryType { get; set; }
        public string CategoryName { get; set; }
        public string CategoryTitle { get; set; }
        public string IconGlyph { get; set; }
        public string IconFamily { get; set; }
        public string PictureUri { get; set; }
       
    }
}
