
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game_Store_Web_Front.Models
{
    public class SetGameDTO
    {
        public string GameName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public int InventoryStock { get; set; }
        public List<SetGenreDTO> Genres { get; set; }
        public List<SetTagDTO> Tags { get; set; }
    }
}