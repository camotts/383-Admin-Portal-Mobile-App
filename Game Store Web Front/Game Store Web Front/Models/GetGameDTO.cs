
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Game_Store_Web_Front.Models
{
    public class GetGameDTO
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string GameName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public int InventoryStock { get; set; }
        public List<GetGenreDTO> Genres { get; set; }
        public List<GetTagDTO> Tags { get; set; }
        public bool check { get; set; }

        [DisplayName("Cover")]
        public string imageSource { get; set; }

    }
}