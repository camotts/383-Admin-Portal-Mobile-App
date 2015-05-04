using System;
using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class AllGames:ContentPage
	{
		string currentGreeting = GetGreeting();
		ListView Lv = new ListView ();
		Label L;

		public AllGames(){

			this.BackgroundImage = "33.jpg";

			var semiTransparentColor = new Color (0, 0, 0, 0.5);
			Padding = new Thickness (6,25,4,6);

			var wishLabel = new Button () { 
				Text = currentGreeting + Application.Current.Properties["FirstName"] +"!",
				BackgroundColor = semiTransparentColor,
				FontSize = 16,
				HeightRequest = 27,
				TextColor = Color.White,
				HorizontalOptions = LayoutOptions.EndAndExpand
					
			};

			var getGamesButton = new Button{ Text = "Get Games" };
			getGamesButton.Clicked += async (sender, e) => {
				var webService = new GameRepository ();
				var webServiceCall = await webService.GetGamesAsync ();
				Lv.ItemsSource= webServiceCall;
				Xamarin.Forms.Device.BeginInvokeOnMainThread (()=>{
					Console.WriteLine ("found size" + webServiceCall.Count);
				});
			};


			Lv.ItemTemplate = new DataTemplate (typeof(TextCell));
			Lv.ItemTemplate.SetBinding (TextCell.TextProperty,"GameName");
			//Console.WriteLine ();

			var layout = new StackLayout {};

			//layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 50});
			layout.Children.Add (wishLabel);
			layout.Children.Add (getGamesButton);
			layout.Children.Add (Lv);

			Content = layout;
		}

		// Greet User
		public static string GetGreeting(){
			string dashGreeting = "";
			int dateTime = DateTime.Now.Hour;

			if (dateTime >= 0 && dateTime <= 11)
			{
				dashGreeting = " Good Morning, ";
			}
			else if (dateTime >= 12 && dateTime <= 17)
			{
				dashGreeting = " Good Afternoon, ";
			}
			else if (dateTime >= 18 && dateTime <= 23)
			{
				dashGreeting = " Good Evening, ";
			}
			else
			{
				dashGreeting = "Hello, ";
			}
			return dashGreeting;
		}
	}

}

