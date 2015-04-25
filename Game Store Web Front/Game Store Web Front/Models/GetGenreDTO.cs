using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game_Store_Web_Front.Models
{
    public class GetGenreDTO
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
        public bool check { get; set; }
    }
}