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
            try
            {
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
                    foreach (var user in x)
                    {
                        user.Id = parseId(user.URL);
                    }
                }
                else if (queryResult.StatusCode == HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("Login", "User");
                }

                return View(x);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error Here", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ListAllUser(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
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


        [HttpGet]
        public ActionResult Create()
        {
            if (!Session["Role"].Equals("Admin"))
            {
                return RedirectToAction("Index", "Home");
            } 
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(SetUserDTO collection)
        {
            if (!Session["Role"].Equals("Admin"))
            {
                return RedirectToAction("Index", "Home");
            } 
            // TODO: Add insert logic here
            var request = new RestRequest("api/Users/", Method.POST);
            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            request.AddObject(collection);
            var queryResult = client.Execute(request);

            statusCodeCheck(queryResult);


            if (queryResult.StatusCode != HttpStatusCode.Created)
            {
                return View();
            }
            else if (queryResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("Login", "User");
            }

            return RedirectToAction("Index");


        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            if (!Session["Role"].Equals("Admin"))
            {
                return RedirectToAction("Index", "Home");
            } 
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
            else if (queryResult.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToAction("Login", "User");
            }
            return View(x);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SetUserDTO collection)
        {
            if (!Session["Role"].Equals("Admin"))
            {
                return RedirectToAction("Index", "Home");
            } 
            try
            {
                // TODO: Add update logic here
                var request = new RestRequest("api/Users/" + id, Method.PUT);
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
                else if (queryResult.StatusCode == HttpStatusCode.Forbidden)
                {
                    return RedirectToAction("Login", "User");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id){
            if (!Session["Role"].Equals("Admin"))
            {
                return RedirectToAction("Index", "Home");
            } 
            try
            {

                // TODO: Add update logic here
                var request = new RestRequest("api/Users/" + id, Method.DELETE);
                var apiKey = Session["ApiKey"];
                var UserId = Session["UserId"];
                request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
                request.AddHeader("xcmps383authenticationid", UserId.ToString());



                var queryResult = client.Execute(request);

                statusCodeCheck(queryResult);
                if (queryResult.StatusCode == HttpStatusCode.Forbidden)
                {
                    var redirectUrl = new UrlHelper(Request.RequestContext).Action("Login", "User");
                    return Json(new { Url = redirectUrl });
                }

                var redirectUrl2 = new UrlHelper(Request.RequestContext).Action("Index", "User");
                return Json(new { Url = redirectUrl2 });
            }
            catch
            {
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "User");
                return Json(new { Url = redirectUrl });
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

            var request = new RestRequest("api/ApiKey?email=" + email + "&password=" + password, Method.GET);
            var queryResult = client.Execute(request);

            if (queryResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                var x = deserial.Deserialize<GetApikeyDTO>(queryResult);
                

                Session["ApiKey"] = x.ApiKey;
                Session["UserId"] = x.UserId;

                request = new RestRequest("api/ApiKey?email=roboAdmin@selu.edu&password=123456", Method.GET);
                queryResult = client.Execute(request);
                var x2 = deserial.Deserialize<GetApikeyDTO>(queryResult);

                request = new RestRequest("api/Users/" + x.UserId, Method.GET);
                

                request.AddHeader("xcmps383authenticationkey", x2.ApiKey);
                request.AddHeader("xcmps383authenticationid", x2.UserId.ToString());
                queryResult = client.Execute(request);

                GetUserDTO user = new GetUserDTO();

                statusCodeCheck(queryResult);



                if (queryResult.StatusCode == HttpStatusCode.OK)
                {
                    user = deserial.Deserialize<GetUserDTO>(queryResult);
                    if (user.Role.ToString().Equals("User"))
                    {
                        Session["Role"] = null;
                    }
                    else
                    {
                        Session["Role"] = user.Role.ToString();
                    }
                    
                    Session["Name"] = user.FirstName;
                }
                else
                {
                    Session["Role"] = "User";
                    Session["Name"] = "Customer";
                }

                
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Content("An error occured with Log In credential!");
            }


        }

        public ActionResult Logout()
        {
            Session["ApiKey"] = null;
            Session["UserId"] = null;
            Session["Role"] = null;
            Session["Name"] = null;
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Home");
            return Json(new { Url = redirectUrl });
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

    }
}
