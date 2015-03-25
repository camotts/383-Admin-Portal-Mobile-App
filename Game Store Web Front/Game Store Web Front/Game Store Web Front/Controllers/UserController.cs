using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Game_Store_Web_Front.Models;

namespace Game_Store_Web_Front.Controllers
{
    public class UserController : Controller
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
                var queryResult = client.Execute(request);

                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();

                List<User> x = deserial.Deserialize<List<User>>(queryResult);

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

            RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
            User x = deserial.Deserialize<User>(queryResult);

            return Json(new { Result = "Ok", Record = x });
        }


       

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User collection)
        {
            try
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


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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

            RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
            User x = deserial.Deserialize<User>(queryResult);
            return View(x);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User collection)
        {
            try
            {
                // TODO: Add update logic here
                var client = new RestClient("http://localhost:12932/");
                var request = new RestRequest("api/Users/", Method.POST);
                var apiKey = Session["ApiKey"];
                var UserId = Session["UserId"];
                request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
                request.AddHeader("xcmps383authenticationid", UserId.ToString());
                request.AddObject(collection);
                var queryResult = client.Execute(request);

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

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {

            var client = new RestClient("http://localhost:12932/");
            var request = new RestRequest("api/ApiKey?email=" + email + "&password=" + password, Method.GET);
            var queryResult = client.Execute(request);

            if (queryResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                var x = deserial.Deserialize<UserLogin>(queryResult);


                Session["ApiKey"] = x.ApiKey;
                Session["UserId"] = x.UserId;


                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Content("An error occured with Log In credential!");
            }
            
           
        }

        public class UserLogin{
            public string ApiKey { get; set; }
            public int UserId { get; set; }
        }
        


    }
}
