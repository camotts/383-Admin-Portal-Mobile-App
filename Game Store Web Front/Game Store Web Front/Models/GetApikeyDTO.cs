using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Game_Store_Web_Front.Models
{
    public class GetApikeyDTO
    {
        public string ApiKey { get; set; }
        public int UserId { get; set; }
    }
}