using Game_Store_Web_Front.Attributes;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game_Store_Web_Front.Controllers
{
    [ExitHttps]
    public abstract class BaseController : Controller
    {
        //comment and uncomment based on needs
        //public RestClient client = new RestClient("http://localhost:12932/");

        public RestClient client = new RestClient("http://dev.envocsupport.com/GameStore2/");
    }
}