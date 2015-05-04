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

			request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

			IRestResponse queryResult = client.Execute(request);

			if (queryResult.StatusCode == HttpStatusCode.OK)
			{
				RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
				var x = deserial.Deserialize<UserApiKey>(queryResult);
							
				Application.Current.Properties["ApiKey"] = x.ApiKey;
				Application.Current.Properties ["UserId"] = x.UserId;

				//Console.WriteLine ("first" + queryResult.Content);
//				Console.WriteLine ("key:"+x.ApiKey);

				// next request for User
				request = new RestRequest ("api/Users/"+x.UserId,Method.GET);

				var ApiKey = Application.Current.Properties ["ApiKey"];
				var UserId = Application.Current.Properties ["UserId"];

				request.AddHeader ("xcmps383authenticationkey",ApiKey.ToString ());
				request.AddHeader ("xcmps383authenticationid",UserId.ToString ());
				queryResult = client.Execute (request);

				User user = new User ();

				statusCodeCheck (queryResult);

				if (queryResult.StatusCode == HttpStatusCode.OK) {
					user = deserial.Deserialize<User>(queryResult);
				}

				Application.Current.Properties ["FirstName"] = user.FirstName;

//				Console.WriteLine (Application.Current.Properties ["UserId"]);
				//Console.WriteLine ("here" + queryResult.Content);


				loginStatus = true;
				return loginStatus ;
			}
			else
			{
				loginStatus = false;
				return loginStatus;

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
				//ModelState.AddModelError("", queryResult.StatusDescription);
				//throw twilioException;
				break;
			default:
				// reaction to HttpStatusCode 
				break;
			}

		}

	
	}
}

