
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;



namespace Game_Store_Web_Front.Models
{
    public class GetSalesDTO
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public DateTime SalesDate { get; set; }
        public decimal Total { get; set; }
        public GetCartDTO Cart { get; set; }


    }
}