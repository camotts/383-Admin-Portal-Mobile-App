using Game_Store_Web_Front.Models;
using RestSharp;
using RestSharp.Deserializers;
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

            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];

            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            var response = client.Execute(request);

            RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
            List<Game> allGames = deserial.Deserialize<List<Game>>(response); // string to object

            return View(allGames);
        }
    }
}