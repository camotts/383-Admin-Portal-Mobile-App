using AutoMapper;
using Game_Store_Web_Front.Attributes;
using Game_Store_Web_Front.DataContext;
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
    public class GameController : BaseController
    {
        // GET: Game
        public dbContext db = new dbContext();
        public ActionResult Index()
        {
            var request = new RestRequest("api/Games", Method.GET);

            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];

            try
            {
                request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
                request.AddHeader("xcmps383authenticationid", UserId.ToString());
            }
            catch { }
           
            List<GetGameDTO> allGames = new List<GetGameDTO>();
            var response = client.Execute(request);

            statusCodeCheck(response);

            if (response.StatusCode == HttpStatusCode.OK)
            {

                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                allGames = deserial.Deserialize<List<GetGameDTO>>(response); // string to object
                foreach (var game in allGames)
                {
                    var thing = db.Images.OrderBy(r => Guid.NewGuid()).Take(1).First();
                    game.imageSource = thing.imageSource;
                    game.Id = parseId(game.URL);
                }
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("Login", "User");
            }
            //todo parse url into id
            return View(allGames);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var temp = Session["Role"];
            if (!Session["Role"].Equals("Admin"))
            {
                return RedirectToAction("Index", "Home");
            } 
            List<GetGenreDTO> genres = new List<GetGenreDTO>();
            List<GetTagDTO> tags = new List<GetTagDTO>();

            genres = getGenres();
            tags = getTags();
            

            ViewBag.genres = genres;
            ViewBag.tags = tags;

            return View();
        }

        [HttpPost]
        public ActionResult Create(SetGameDTO createGame)
        {
            if (!Session["Role"].Equals("Admin"))
            {
                return RedirectToAction("Index", "Home");
            } 
            var request = new RestRequest("api/Games", Method.POST);
            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            request.RequestFormat = DataFormat.Json;
            request.AddBody(createGame);
            var queryResult = client.Execute(request);
            statusCodeCheck(queryResult);

            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Create", "Game");


            if (queryResult.StatusCode != HttpStatusCode.Created)
            {
                redirectUrl = new UrlHelper(Request.RequestContext).Action("Create", "Game");
                return Json(new { Url = redirectUrl });
            }
            else if (queryResult.StatusCode == HttpStatusCode.Forbidden)
            {
                redirectUrl = new UrlHelper(Request.RequestContext).Action("Login", "User");
                return Json(new { Url = redirectUrl });
            }
            redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Game");
            return Json(new { Url = redirectUrl });

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (!Session["Role"].Equals("Admin"))
            {
                return RedirectToAction("Index", "Home");
            } 
            var request = new RestRequest("api/Games/" + id, Method.GET);
            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            var queryResult = client.Execute(request);

            List<GetGenreDTO> genres = new List<GetGenreDTO>();
            List<GetTagDTO> tags = new List<GetTagDTO>();
            genres = getGenres();
            tags = getTags();
            

            GetGameDTO game = new GetGameDTO();

            statusCodeCheck(queryResult);



            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                game = deserial.Deserialize<GetGameDTO>(queryResult);
                foreach (GetTagDTO thing in tags)
                {
                    foreach (GetTagDTO compared in game.Tags)
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

                foreach (GetGenreDTO thing in genres)
                {
                    foreach (GetGenreDTO compared in game.Genres)
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
            else if (queryResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("Login", "User");
            }

                game.Id = parseId(game.URL);

            ViewBag.genres = genres;
            ViewBag.tags = tags;
            return View(game);
        }

        [HttpPost]
        public ActionResult Edit(GetGameDTO editedGame, string url)
        {
            if (!Session["Role"].Equals("Admin"))
            {
                return RedirectToAction("Index", "Home");
            } 
            try
            {
                Mapper.CreateMap<SetGameDTO, GetGameDTO>();
                // TODO: Add update logic here

                var client = new RestClient(editedGame.URL);
                var request = new RestRequest(Method.PUT);
                var apiKey = Session["ApiKey"];
                var UserId = Session["UserId"];
                request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
                request.AddHeader("xcmps383authenticationid", UserId.ToString());
                request.RequestFormat = DataFormat.Json;

                SetGameDTO sentGame = new SetGameDTO();
                sentGame.GameName = editedGame.GameName;
                sentGame.Genres = new List<SetGenreDTO>();
                foreach (var genre in editedGame.Genres)
                {
                    sentGame.Genres.Add(new SetGenreDTO() { Name = genre.Name });
                }
                sentGame.InventoryStock = editedGame.InventoryStock;
                sentGame.Price = editedGame.Price;
                sentGame.ReleaseDate = editedGame.ReleaseDate;
                sentGame.Tags = new List<SetTagDTO>();
                foreach (var tag in editedGame.Tags)
                {
                    sentGame.Tags.Add(new SetTagDTO() { Name = tag.Name });
                }

                request.AddBody(sentGame);
                
                var queryResult = client.Execute(request);

                statusCodeCheck(queryResult);

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Game");

                if (queryResult.StatusCode != HttpStatusCode.OK)
                {
                    redirectUrl = new UrlHelper(Request.RequestContext).Action("Edit/"+editedGame.Id, "Game");
                    return Json(new { Url = redirectUrl });
                }
                else if (queryResult.StatusCode == HttpStatusCode.Forbidden)
                {
                    redirectUrl = new UrlHelper(Request.RequestContext).Action("Login", "User");
                    return Json(new { Url = redirectUrl });
                }
                redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Game");
                return Json(new { Url = redirectUrl });
            }
            catch 
            {
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Edit/"+editedGame.Id, "Game");
                return Json(new { Url = redirectUrl });
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!Session["Role"].Equals("Admin"))
            {
                return RedirectToAction("Index", "Home");
            } 
            try
            {
                
                // TODO: Add update logic here
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

        public List<GetGenreDTO> getGenres()
        {
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
                return deserial.Deserialize<List<GetGenreDTO>>(queryResult);
            }

            return null;
        }

        public List<GetTagDTO> getTags()
        {

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
                return deserial.Deserialize<List<GetTagDTO>>(queryResult);
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

        

    }
}