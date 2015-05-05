using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Game_Store_Web_Front.Models;
using System.Net;
using AutoMapper;
using Newtonsoft.Json;
using Game_Store_Web_Front.DataContext;
using Game_Store_Web_Front.Attributes;

namespace Game_Store_Web_Front.Controllers
{

    public class SaleController : BaseController
    {
        
        public dbContext db = new dbContext();

        

        //get Cart
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult listAllCarts()
        {


            return View(getCarts());
        }

        [HttpGet]
        public ActionResult create(int id)
        {
            GetCartDTO cart = getSpecificSellCart(id);
            return PartialView(cart);
        }

        [HttpGet]
        public ActionResult createSale(int id)
        {

            return View(getSpecificSellCart(id));
        }


        [HttpPost]
        public ActionResult createSale(int id, string employee)
        {
            Mapper.CreateMap<SetCartDTO, GetCartDTO>();
            Mapper.CreateMap<SetGameDTO, GetGameDTO>();
            Mapper.CreateMap<SetGenreDTO, GetGenreDTO>();
            Mapper.CreateMap<SetTagDTO, GetTagDTO>();

            GetCartDTO sellCart = getSpecificSellCart(id);
            //use automapper to bind incoming to outgoing
            //SetCartDTO outgoingCart = Mapper.Map<SetCartDTO>(sellCart);

            List<SetGameDTO> cartGames = new List<SetGameDTO>();
            SetCartDTO outgoingCart = new SetCartDTO();
            //manualy parse to an SetCartDTO

            outgoingCart.User_Id = parseId(sellCart.URL);

            /*
            foreach (GetGameDTO game in sellCart.Games)
            {
                SetGameDTO tempGame = new SetGameDTO();
                foreach (GetGenreDTO genre in game.Genres)
                {
                    tempGame.Genres.Add(Mapper.Map<SetGenreDTO>(genre));
                }
                foreach (GetTagDTO tag in game.Tags)
                {
                    tempGame.Tags.Add(Mapper.Map<SetTagDTO>(tag));
                }
                tempGame.GameName = game.GameName;
                tempGame.InventoryStock = game.InventoryStock;
                tempGame.Price = game.Price;
                tempGame.ReleaseDate = game.ReleaseDate;
                cartGames.Add(tempGame);
            }
             */

            //outgoingCart.Games = cartGames;




            var request = new RestRequest("api/Sales", Method.POST);
            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());

            request.AddObject(outgoingCart);
            var queryResult = client.Execute(request);
            statusCodeCheck(queryResult);

            if (queryResult.StatusCode != HttpStatusCode.Created)
            {
                return RedirectToAction("createSale", parseId(sellCart.URL));
            }
            else if (queryResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("Login", "User");
            }

            return RedirectToAction("Index", "Home", "");
        }

        public ActionResult listAllSales()
        {

            var request = new RestRequest("api/Sales", Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            IRestResponse queryResult = client.Execute(request);
            List<GetSalesDTO> x = new List<GetSalesDTO>();

            statusCodeCheck(queryResult);

            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                x = JsonConvert.DeserializeObject<List<GetSalesDTO>>(queryResult.Content);
                foreach (var sale in x)
                {
                    sale.Id = parseId(sale.URL);
                }
            }
            else if (queryResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("Login", "User");
            }

            return View(x);



        }

        public ActionResult Details(int id)
        {

            var request = new RestRequest("api/Carts/" + id, Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };


            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            request.AddQueryParameter("checkout", "true");
            IRestResponse queryResult = client.Execute(request);
            GetCartDTO x = new GetCartDTO();

            statusCodeCheck(queryResult);

            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                x = JsonConvert.DeserializeObject<GetCartDTO>(queryResult.Content);
            }
            else if (queryResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("Login", "User");
            }

            return View(x);

        }

        public void Sale(int id)
        {
            //get the cart we are selling
            
            var request = new RestRequest("api/Carts/" + id, Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            request.AddHeader("checkout", "true");
            IRestResponse queryResult = client.Execute(request);
            GetCartDTO x = new GetCartDTO();

            statusCodeCheck(queryResult);


            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                x = JsonConvert.DeserializeObject<GetCartDTO>(queryResult.Content);

            }
            else if (queryResult.StatusCode == HttpStatusCode.Forbidden)
            {
            }

            //process the sale
            request = new RestRequest("api/Sales/", Method.POST);
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.RequestFormat = DataFormat.Json;
            request.AddBody(x);
            queryResult = client.Execute(request);


            statusCodeCheck(queryResult);

            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                x = JsonConvert.DeserializeObject<GetCartDTO>(queryResult.Content);

            }
            else if (queryResult.StatusCode == HttpStatusCode.Forbidden)
            {
            }


        }

        [HttpGet]
        public ActionResult Payment(int id)
        {

            GetCartDTO thing = getSpecificSellCart(id);
            decimal price = 0;
            foreach (var game in thing.Games)
            {
                price += game.Item1.Price;
            }
            Session["User"] = thing.User_Id;
            ViewBag.Price = price;
            return PartialView();
        }

        [HttpPost]
        public void Payment(Payment info)
        {
            if (!ModelState.IsValid)
            {
            }
            info.UserId = Convert.ToInt32(Session["User"]);
            try
            {
                db.Payments.Add(info);
                db.SaveChanges();
            }
            catch { }
            Session["User"] = null;
            Sale(info.UserId);

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            var request = new RestRequest("api/Sales/" + id, Method.GET);
            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            var queryResult = client.Execute(request);
            List<GetGameDTO> games = getGames();
            GetSalesDTO x = new GetSalesDTO();
            GetSalesDTO send = new GetSalesDTO();

            statusCodeCheck(queryResult);


            ViewBag.games = games;

            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                x = JsonConvert.DeserializeObject<GetSalesDTO>(queryResult.Content);

                x.Id = parseId(x.URL);
                send.Id = x.Id;
                send.SalesDate = x.SalesDate;
                send.Total = x.Total;
                send.URL = x.URL;
                send.Cart = new GetCartDTO();
                send.Cart.Games = new List<Tuple<GetGameDTO, int>>();
                foreach (var game in games)
                {
                    foreach (var thing in x.Cart.Games)
                    {
                        if (game.GameName != null)
                        {
                            if (game.GameName.Equals(thing.Item1.GameName))
                            {
                                game.check = true;

                            }
                        }
                    }
                }

                foreach (GetGameDTO game in games)
                {
                    if (x.Cart.Games.FirstOrDefault(m => m.Item1.GameName == game.GameName) != null)
                    {
                        send.Cart.Games.Add(Tuple.Create(game, x.Cart.Games.Where(m => m.Item1.GameName == game.GameName).FirstOrDefault().Item2));
                    }
                    else
                    {
                        send.Cart.Games.Add(Tuple.Create(game, 0));
                    }
                }

            }
            else if (queryResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("Login", "User");
            }
            return View(send);
        }

        [HttpPost]
        public ActionResult Edit(GetSalesDTO editedSale)
        {

            return null;
        }

        public void statusCodeCheck(IRestResponse queryResult)
        {
            switch (queryResult.StatusCode)
            {
                case HttpStatusCode.OK:
                    // reaction to HttpStatusCode 
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
            return Convert.ToInt32(parsed[parsed.Length - 1]);
        }

        public GetCartDTO getSpecificSellCart(int id)
        {

            var request = new RestRequest("api/Carts/" + id, Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            request.AddParameter("checkout", "huuuurrrrrduuuuurrrr");
            IRestResponse queryResult = client.Execute(request);
            GetCartDTO x = new GetCartDTO();

            statusCodeCheck(queryResult);

            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                x = JsonConvert.DeserializeObject<GetCartDTO>(queryResult.Content);
                x.Id = parseId(x.URL);
                foreach (var game in x.Games)
                {
                    var thing = db.Images.OrderBy(r => Guid.NewGuid()).Take(1).First();
                    game.Item1.imageSource = thing.imageSource;
                }
            }
            return x;
        }
        public List<GetCartDTO> getCarts()
        {

            var request = new RestRequest("api/Carts", Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            IRestResponse queryResult = client.Execute(request);
            List<GetCartDTO> x = new List<GetCartDTO>();

            statusCodeCheck(queryResult);

            if (queryResult.StatusCode == HttpStatusCode.OK)               
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                x = JsonConvert.DeserializeObject<List<GetCartDTO>>(queryResult.Content);
                foreach (var cart in x)
                {
                    cart.Id = parseId(cart.URL);
                    foreach (var game in cart.Games)
                    {
                        var thing = db.Images.OrderBy(r => Guid.NewGuid()).Take(1).First();
                        game.Item1.imageSource = thing.imageSource;
                    }
                }
            }


            return x;
        }
        public List<GetGameDTO> getGames()
        {

            var request = new RestRequest("api/Games", Method.GET);

            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];

            request = new RestRequest("api/Games", Method.GET);
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());

            var queryResult = client.Execute(request);

            statusCodeCheck(queryResult);

            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                return deserial.Deserialize<List<GetGameDTO>>(queryResult);
            }


            return null;
        }

    }
}
