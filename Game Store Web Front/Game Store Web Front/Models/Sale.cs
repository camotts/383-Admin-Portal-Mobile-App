using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Store_Web_Front.Models
{
    class Sale
    {
        public string URL { get; set; }
        public int Id { get; set; }
        public DateTime SalesDate { get; set; }
        public decimal Total { get; set; }
        public Cart Cart { get; set; }
    }
}
