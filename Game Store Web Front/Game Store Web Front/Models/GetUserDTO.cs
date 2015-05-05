
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;


namespace Game_Store_Web_Front.Models
{
    public class GetUserDTO
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
    }
}