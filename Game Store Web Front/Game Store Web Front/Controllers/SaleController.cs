using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Game_Store_Web_Front.Models;
using System.Net;

namespace Game_Store_Web_Front.Controllers
{
    public class SaleController : Controller
    {


        //get Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult listAllSales()
        {
            var client = new RestClient("http://dev.envocsupport.com/GameStore2/");
            var request = new RestRequest("api/Sales", Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            IRestResponse queryResult = client.Execute(request);
            List<Sale> x = new List<Sale>();

            statusCodeCheck(queryResult);

            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                x = deserial.Deserialize<List<Sale>>(queryResult);
            }

            return View(x);


            
        }

        public ActionResult Details(int id)
        {
            var client = new RestClient("http://dev.envocsupport.com/GameStore2/");
            var request = new RestRequest("api/Carts/"+id, Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            IRestResponse queryResult = client.Execute(request);
            Cart x = new Cart();

            statusCodeCheck(queryResult);

            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                x = deserial.Deserialize<Cart>(queryResult);
            }

            return View(x);
        
        }

        public ActionResult Sale(int id)
        {
            //get the cart we are selling
            var client = new RestClient("http://dev.envocsupport.com/GameStore2/");
            var request = new RestRequest("api/Carts/" + id, Method.GET);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            IRestResponse queryResult = client.Execute(request);
            Cart x = new Cart();

            statusCodeCheck(queryResult);


            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                x = deserial.Deserialize<Cart>(queryResult);
            }
            
            //process the sale
            request = new RestRequest("api/Sales/", Method.POST);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.AddBody(x);
            queryResult = client.Execute(request);


            statusCodeCheck(queryResult);

            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                x = deserial.Deserialize<Cart>(queryResult);

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

    }
}
