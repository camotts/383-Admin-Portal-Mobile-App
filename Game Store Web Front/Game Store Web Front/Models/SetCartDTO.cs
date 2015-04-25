
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Game_Store_Web_Front.Models
{
    public class SetCartDTO
    {
        public int User_Id { get; set; }
        public List<Tuple<SetGameDTO, int>> Games { get; set; }

    }
}