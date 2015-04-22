using AutoMapper;
using Game_Store_Web_Front.Models;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Game_Store_Web_Front.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            var client = new RestClient("http://dev.envocsupport.com/GameStore2/");
            var request = new RestRequest("api/Games", Method.GET);

            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];

            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
           
            List<Game> allGames = new List<Game>();
            var response = client.Execute(request);

            statusCodeCheck(response);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                allGames = deserial.Deserialize<List<Game>>(response); // string to object
                foreach (var game in allGames)
                {
                    game.Id = parseId(game.URL);
                }
            }
            //todo parse url into id
            return View(allGames);
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<Genre> genres = new List<Genre>();
            List<Tag> tags = new List<Tag>();

            genres = getGenres();
            tags = getTags();
            

            ViewBag.genres = genres;
            ViewBag.tags = tags;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Game createGame)
        {
            var client = new RestClient("http://dev.envocsupport.com/GameStore2/");
            var request = new RestRequest("api/Games", Method.POST);
            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            request.AddObject(createGame);
            var queryResult = client.Execute(request);
            statusCodeCheck(queryResult);

            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Create", "Game");


            if (queryResult.StatusCode != HttpStatusCode.Created)
            {
                redirectUrl = new UrlHelper(Request.RequestContext).Action("Create", "Game");
                return Json(new { Url = redirectUrl });
            }
            redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Game");
            return Json(new { Url = redirectUrl });

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var client = new RestClient("http://dev.envocsupport.com/GameStore2/");
            var request = new RestRequest("api/Games/" + id, Method.GET);
            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            var queryResult = client.Execute(request);

            List<Genre> genres = new List<Genre>();
            List<Tag> tags = new List<Tag>();
            genres = getGenres();
            tags = getTags();
            

            Game game = new Game();

            statusCodeCheck(queryResult);



            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                game = deserial.Deserialize<Game>(queryResult);
                foreach (Tag thing in tags)
                {
                    foreach (Tag compared in game.Tags)
                    {
                        if (thing.Name != null)
                        {
                            if (thing.Name.Equals(compared.Name))
                            {
                                thing.check = true;
                            }
                        }
                    }
                }

                foreach (Genre thing in genres)
                {
                    foreach (Genre compared in game.Genres)
                    {
                        if (thing.Name != null)
                        {
                            if (thing.Name.Equals(compared.Name))
                            {
                                thing.check = true;
                            }
                        }
                    }
                }
            }
            ViewBag.genres = genres;
            ViewBag.tags = tags;
            return View(game);
        }

        [HttpPost]
        public ActionResult Edit(Game editedGame)
        {
            try
            {
                // TODO: Add update logic here
                Mapper.CreateMap<Game, outGame>();

                var client = new RestClient("http://dev.envocsupport.com/GameStore2/");
                var request = new RestRequest("api/Games/"+editedGame.Id, Method.PUT);
                var apiKey = Session["ApiKey"];
                var UserId = Session["UserId"];
                request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
                request.AddHeader("xcmps383authenticationid", UserId.ToString());

                outGame outgoingGame = new outGame();
                outgoingGame = Mapper.Map<outGame>(editedGame);
                request.AddObject(outgoingGame);

                var queryResult = client.Execute(request);

                statusCodeCheck(queryResult);



                if (queryResult.StatusCode != HttpStatusCode.OK)
                {
                    return View();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                
                // TODO: Add update logic here
                var client = new RestClient("http://dev.envocsupport.com/GameStore2/");
                var request = new RestRequest("api/Games/" + id, Method.DELETE);
                var apiKey = Session["ApiKey"];
                var UserId = Session["UserId"];
                request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
                request.AddHeader("xcmps383authenticationid", UserId.ToString());

                
               
                var queryResult = client.Execute(request);

                statusCodeCheck(queryResult);

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Game");
                return Json(new { Url = redirectUrl });
            }
            catch
            {
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Game");
                return Json(new { Url = redirectUrl });
            }
        }

        public List<Genre> getGenres()
        {
            var client = new RestClient("http://dev.envocsupport.com/GameStore2/");
            var request = new RestRequest("api/Genres", Method.GET);

            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];

            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            var queryResult = client.Execute(request);

            statusCodeCheck(queryResult);



            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                return deserial.Deserialize<List<Genre>>(queryResult);
            }
            return null;
        }

        public List<Tag> getTags()
        {
            var client = new RestClient("http://dev.envocsupport.com/GameStore2/");
            var request = new RestRequest("api/Genres", Method.GET);

            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];

            request = new RestRequest("api/Tags", Method.GET);
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());

            var queryResult = client.Execute(request);

            statusCodeCheck(queryResult);

            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                return deserial.Deserialize<List<Tag>>(queryResult);
            }

            return null;
        }

        public void statusCodeCheck(IRestResponse queryResult)
        {
            switch (queryResult.StatusCode)
            {
                case HttpStatusCode.OK:
                    // reaction to HttpStatusCode 
                    break;
                case HttpStatusCode.Created:
                    //reaction
                    break;
                case HttpStatusCode.NotFound:
                    // reaction to HttpStatusCode 
                    break;
                case HttpStatusCode.Forbidden:
                    // reaction to HttpStatusCode 
                    const string message = "Error retrieving response.  Check inner details for more info.";
                    var twilioException = new ApplicationException(message, queryResult.ErrorException);
                    //throw new ApplicationException(queryResult.ErrorMessage);
                    ModelState.AddModelError("", queryResult.StatusDescription);
                    //throw twilioException;
                    break;
                default:
                    // reaction to HttpStatusCode 
                    break;
            }

        }

        public int parseId(string url)
        {
            string[] parsed = url.Split('/');
            return Convert.ToInt32(parsed[parsed.Length-1]);
        }

        public class outGame{
            public string URL { get; set; }
            public int Id { get; set; }
            public string GameName { get; set; }
            public DateTime ReleaseDate { get; set; }
            public decimal Price { get; set; }
            public int InventoryStock { get; set; }
            public IEnumerable<Genre> Genres { get; set; }
            public IEnumerable<Tag> Tags { get; set; }
        }
    }
}