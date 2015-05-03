
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Web;



namespace Game_Store_Web_Front.Models
{
    public class GetSalesDTO
    {
        public int Id { get; set; }
        public string URL { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SalesDate { get; set; }
        public decimal Total { get; set; }
        public GetCartDTO Cart { get; set; }


    }
}