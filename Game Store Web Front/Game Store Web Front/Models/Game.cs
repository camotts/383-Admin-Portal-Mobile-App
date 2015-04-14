using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Game_Store_Web_Front.Models
{
    public class Game
    {
        public string URL { get; set; }
        public int Id { get; set; }
        public string GameName { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public int InventoryStock { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Tag> Tags { get; set; }
    }
}