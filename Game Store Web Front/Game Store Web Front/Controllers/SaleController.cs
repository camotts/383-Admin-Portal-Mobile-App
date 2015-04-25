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

namespace Game_Store_Web_Front.Controllers
{
    public class SaleController : Controller
    {
        

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
             




            var client = new RestClient("http://localhost:12932/");
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

            return RedirectToAction("Index", "Home", "");
        }

        public ActionResult listAllSales()
        {
            var client = new RestClient("http://localhost:12932/");
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
            }

            return View(x);


            
        }

        public ActionResult Details(int id)
        {
            var client = new RestClient("http://localhost:12932/");
            var request = new RestRequest("api/Carts/"+id, Method.GET);
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

            return View(x);
        
        }

        public ActionResult Sale(int id)
        {
            //get the cart we are selling
            var client = new RestClient("http://localhost:12932/");
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

            return RedirectToAction("Index");

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
            var client = new RestClient("http://localhost:12932/");
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
            }
            return x;
        }
        public List<GetCartDTO> getCarts()
        {
            var client = new RestClient("http://localhost:12932/");
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
                }
            }
            return x;
        }

    }
}
