
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;


namespace Game_Store_Web_Front.Models
{
    public class SetSalesDTO
    {
        public DateTime SalesDate { get; set; }
        public SetCartDTO Cart { get; set; }
    }
}