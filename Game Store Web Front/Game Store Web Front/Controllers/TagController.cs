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
    public class TagController : Controller
    {
        // GET: Tag
        public ActionResult Index()
        {
            List<Tag> tags = new List<Tag>();
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
                tags = deserial.Deserialize<List<Tag>>(queryResult);
                foreach (var tag in tags)
                {
                    tag.Id = parseId(tag.URL);
                }
            }

            return View(tags);
        }

        // GET: Tag/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tag/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tag/Create
        [HttpPost]
        public ActionResult Create(Tag collection)
        {
            var client = new RestClient("http://dev.envocsupport.com/GameStore2/");
            var request = new RestRequest("api/Tags/", Method.POST);
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

            return RedirectToAction("Index");
        }

        // GET: Tag/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient("http://dev.envocsupport.com/GameStore2/");
            var request = new RestRequest("api/Tags/" + id, Method.GET);
            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            var queryResult = client.Execute(request);

            Tag x = new Tag();

            statusCodeCheck(queryResult);



            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                x = deserial.Deserialize<Tag>(queryResult);
            }
            return View(x);
        }

        // POST: Tag/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Tag collection)
        {
            try
            {
                // TODO: Add update logic here
                var client = new RestClient("http://dev.envocsupport.com/GameStore2/");
                var request = new RestRequest("api/Tags/" + id, Method.PUT);
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


        // POST: Tag/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add update logic here
                var client = new RestClient("http://dev.envocsupport.com/GameStore2/");
                var request = new RestRequest("api/Tags/" + id, Method.DELETE);
                var apiKey = Session["ApiKey"];
                var UserId = Session["UserId"];
                request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
                request.AddHeader("xcmps383authenticationid", UserId.ToString());


                var queryResult = client.Execute(request);

                statusCodeCheck(queryResult);

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Tag");
                return Json(new { Url = redirectUrl });
            }
            catch
            {
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Tag");
                return Json(new { Url = redirectUrl });
            }
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
            return Convert.ToInt32(parsed[parsed.Length - 1]);
        }
    }
}
