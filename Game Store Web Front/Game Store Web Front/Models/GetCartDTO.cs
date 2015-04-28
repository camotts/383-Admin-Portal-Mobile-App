
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Game_Store_Web_Front.Models
{
    public class GetCartDTO
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public bool CheckoutReady { get; set; }
        public int User_Id { get; set; }
        public List<Tuple<GetGameDTO, int>> Games { get; set; }

    }
}