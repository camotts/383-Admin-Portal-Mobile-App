using System;
using Xamarin.Forms;
using RestSharp;
using RestSharp.Deserializers;
using System.Net;

namespace GameStoreMobileApp
{
	public class UserRepository
	{
		public bool loginStatus=false;
		public UserRepository ()
		{

		}

	
		public Boolean OnLogInButtonClicked (string email, string password)
		{
			var client = new RestClient("http://dev.envocsupport.com/GameStore2/");
			var request = new RestRequest("api/ApiKey?email=" + email + "&password=" + password, Method.GET);

			var queryResult = client.Execute(request);

			if (queryResult.StatusCode == HttpStatusCode.OK)
			{
//				var deserial = new JsonDeserializer();
//				var x = deserial.Deserialize<ApiKey>(queryResult);
//
//				var storeCookies = new CookieContainer();
//				client.CookieContainer = storeCookies;
			
				loginStatus = true;
				return loginStatus ;
			}
			else
			{
				loginStatus = false;
				return loginStatus;
				//DisplayAlert ("Invalid Credentials", "Please Check your Email or Password!", "OK");
			}

		}
	}
}

