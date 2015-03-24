using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game_Store_Web_Front.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            var client = new RestClient("http://localhost:12932/");
            var request = new RestRequest("api/Games", Method.GET);

            return null;
        }
    }
}