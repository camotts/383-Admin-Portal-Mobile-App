using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Store_Web_Front.Models
{
    public class Cart
    {
        public string URL { get; set; }
        public int Id { get; set; }
        public bool CheckoutReady { get; set; }
        public int User_Id { get; set; }
        public List<Game> Games { get; set; }
    }
}
