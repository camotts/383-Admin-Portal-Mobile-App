using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Game_Store_Web_Front.Models;
using System.Net;
using Game_Store_Web_Front.Attributes;


namespace Game_Store_Web_Front.Controllers
{
    public class UserController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListAllUser(int jtStartIndex=0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var client = new RestClient("http://localhost:12932/");
                var request = new RestRequest("api/Users", Method.GET);
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                var apiKey = Session["ApiKey"];
                var UserId = Session["UserId"];
                request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
                request.AddHeader("xcmps383authenticationid", UserId.ToString());
                IRestResponse queryResult = client.Execute(request);
                List<GetUserDTO> x = new List<GetUserDTO>();

                statusCodeCheck(queryResult);

                if (queryResult.StatusCode == HttpStatusCode.OK)
                {
                    RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                    x = deserial.Deserialize<List<GetUserDTO>>(queryResult);
                }

                return Json(new { Result = "OK", Records = x }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) 
            {
                return Json(new { Result = "Error Here", Message = ex.Message });
            }
        }

        // GET: User/Details/5
        public JsonResult Details(int id)
        {
            var client = new RestClient("http://localhost:12932/");
            var request = new RestRequest("api/Users/" + id, Method.GET);
            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            var queryResult = client.Execute(request);

            GetUserDTO x = new GetUserDTO();

            statusCodeCheck(queryResult);


            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                x = deserial.Deserialize<GetUserDTO>(queryResult);
            }

            return Json(new { Result = "Ok", Record = x });
        }


       

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(SetUserDTO collection)
        {
            
                // TODO: Add insert logic here
                var client = new RestClient("http://localhost:12932/");
                var request = new RestRequest("api/Users/", Method.POST);
                var apiKey = Session["ApiKey"];
                var UserId = Session["UserId"];
                request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
                request.AddHeader("xcmps383authenticationid", UserId.ToString());
                request.AddObject(collection);
                var queryResult = client.Execute(request);

                statusCodeCheck(queryResult);


                if (queryResult.StatusCode != HttpStatusCode.OK)
                {
                    return View();
                }

                return RedirectToAction("Index");
            
          
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient("http://localhost:12932/");
            var request = new RestRequest("api/Users/" + id, Method.GET);
            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            var queryResult = client.Execute(request);

            GetUserDTO x = new GetUserDTO();

            statusCodeCheck(queryResult);



            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                x = deserial.Deserialize<GetUserDTO>(queryResult);
            }
            return View(x);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SetUserDTO collection)
        {
            try
            {
                // TODO: Add update logic here
                var client = new RestClient("http://localhost:12932/");
                var request = new RestRequest("api/Users/"+id, Method.PUT);
                var apiKey = Session["ApiKey"];
                var UserId = Session["UserId"];
                request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
                request.AddHeader("xcmps383authenticationid", UserId.ToString());
                request.AddObject(collection);
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var client = new RestClient("http://localhost:12932/");
                var request = new RestRequest("api/Users/" + id, Method.DELETE);
                var apiKey = Session["ApiKey"];
                var UserId = Session["UserId"];
                request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
                request.AddHeader("xcmps383authenticationid", UserId.ToString());
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

        [HttpGet]
        [RequireSSL]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [RequireSSL]
        public ActionResult Login(string email, string password)
        {

            var client = new RestClient("http://localhost:12932/");
            var request = new RestRequest("api/ApiKey?email=" + email + "&password=" + password, Method.GET);
            var queryResult = client.Execute(request);

            if (queryResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                var x = deserial.Deserialize<GetApikeyDTO>(queryResult);


                Session["ApiKey"] = x.ApiKey;
                Session["UserId"] = x.UserId;


                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Content("An error occured with Log In credential!");
            }
            
           
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
