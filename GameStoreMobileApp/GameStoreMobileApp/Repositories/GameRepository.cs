using System;
using System.Threading.Tasks;
using RestSharp;
using Xamarin.Forms;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace GameStoreMobileApp
{
	public class GameRepository
	{
		public GameRepository ()
		{
		}

		public async Task<List<Game>> GetGamesAsync(){
			Application.Current.Properties ["CartQuantity"] = "0";
			var client = new RestClient("http://dev.envocsupport.com/GameStore2/");
			var request = new RestRequest ("api/Games", Method.GET);
			request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

			var apiKey = Application.Current.Properties ["ApiKey"];
			var userId = Application.Current.Properties ["UserId"];

			try
			{
				request.AddHeader ("xcmps383authenticationkey",apiKey.ToString ());
				request.AddHeader ("xcmps383authenticationid",userId.ToString ());
			} 
			catch{}

			IRestResponse response = client.Execute(request);

			statusCodeCheck (response);
			Console.WriteLine ("here" + response.Content);

			if (response.StatusCode == HttpStatusCode.OK) {

				var rootObject = new RestSharp.Deserializers.JsonDeserializer().Deserialize<List<Game>>(response);
				return rootObject;
			}
			else {
				return null;
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

