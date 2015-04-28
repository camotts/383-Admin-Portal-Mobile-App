﻿using Game_Store_Web_Front.Models;
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
    public class GenreController : BaseController
    {
        public ActionResult Index()
        {
            List<GetGenreDTO> genres = new List<GetGenreDTO>();
            var client = new RestClient("http://localhost:12932/");
            var request = new RestRequest("api/Genres", Method.GET);

            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];

            request = new RestRequest("api/Genres", Method.GET);
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());

            var queryResult = client.Execute(request);

            statusCodeCheck(queryResult);

            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                genres = deserial.Deserialize<List<GetGenreDTO>>(queryResult);
                foreach (var genre in genres)
                {
                    genre.Id = parseId(genre.URL);
                }
            }

            return View(genres);
        }

        // GET: Genre/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Genre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
        [HttpPost]
        public ActionResult Create(SetGenreDTO collection)
        {
            var client = new RestClient("http://localhost:12932/");
            var request = new RestRequest("api/Genres/", Method.POST);
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

        // GET: Genre/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient("http://localhost:12932/");
            var request = new RestRequest("api/Genres/" + id, Method.GET);
            var apiKey = Session["ApiKey"];
            var UserId = Session["UserId"];
            request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
            request.AddHeader("xcmps383authenticationid", UserId.ToString());
            var queryResult = client.Execute(request);

            GetGenreDTO x = new GetGenreDTO();

            statusCodeCheck(queryResult);



            if (queryResult.StatusCode == HttpStatusCode.OK)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                x = deserial.Deserialize<GetGenreDTO>(queryResult);
                
                x.Id = parseId(x.URL);
                
            }
            return View(x);
        }

        // POST: Genre/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SetGenreDTO collection)
        {
            try
            {
                // TODO: Add update logic here
                var client = new RestClient("http://localhost:12932/");
                var request = new RestRequest("api/Genres/" + id, Method.PUT);
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


        // POST: Genre/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add update logic here
                var client = new RestClient("http://localhost:12932/");
                var request = new RestRequest("api/Genres/" + id, Method.DELETE);
                var apiKey = Session["ApiKey"];
                var UserId = Session["UserId"];
                request.AddHeader("xcmps383authenticationkey", apiKey.ToString());
                request.AddHeader("xcmps383authenticationid", UserId.ToString());


                var queryResult = client.Execute(request);

                statusCodeCheck(queryResult);

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Genre");
                return Json(new { Url = redirectUrl });
            }
            catch
            {
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Genre");
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
