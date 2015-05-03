using System;
using Xamarin.Forms;
using RestSharp;
using RestSharp.Deserializers;
using System.Net;
using System.Threading.Tasks;

namespace GameStoreMobileApp
{
	public class UserRepository
	{
		public bool loginStatus=false;
		public UserRepository ()
		{

		}
			
		public  Boolean OnLogInButtonClicked (string email, string password)
		{
			var hud = DependencyService.Get<IHud> ();
			hud.Show ("Verifying Credentials");
			var client = new RestClient("http://dev.envocsupport.com/GameStore2/");
			var request = new RestRequest("api/ApiKey?email=" + email + "&password=" + password, Method.GET);

			var queryResult = client.Execute(request);

			if (queryResult.StatusCode == HttpStatusCode.OK)
			{
				var deserial = new JsonDeserializer();
				var x = deserial.Deserialize<UserApiKey>(queryResult);
							
				Application.Current.Properties["ApiKey"] = x.ApiKey;
				Application.Current.Properties ["UserId"] = x.UserId;

//				Console.WriteLine (queryResult.Content);
//				Console.WriteLine ("key:"+x.ApiKey);

				loginStatus = true;
				return loginStatus ;
			}
			else
			{
				loginStatus = false;
				return loginStatus;

			}

		}

	
	}
}

