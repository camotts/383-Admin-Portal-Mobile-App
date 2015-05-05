using System;
using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class AllGames:ContentPage
	{
		string currentGreeting = GetGreeting ();
		ListView Lv = new ListView ();
		Label L;

		public AllGames ()
		{

			//this.BackgroundImage = "33.jpg";
			this.Title="ANFIELD";
			var semiTransparentColor = new Color (0, 0, 0, 0.5);
			Padding = new Thickness (6, 0, 4, 6);

			var label = new Label { 
				Text="THE GAME SHOP",
				FontSize = 24,
				HorizontalOptions = LayoutOptions.Center,
				TextColor = Color.Accent
			};
			var wishLabel = new Button () { 
				Text = currentGreeting + Application.Current.Properties ["FirstName"] + "!",
				BackgroundColor = semiTransparentColor,
				FontSize = 16,
				HeightRequest = 27,
				TextColor = Color.White,
				HorizontalOptions = LayoutOptions.EndAndExpand
					
			};

			var getGamesButton = new Button{ Text = "" };

//			getGamesButton.Clicked += async (sender, e) => {
//
//				var webService = new GameRepository ();
//				var webServiceCall = await webService.GetGamesAsync ();
//				Lv.ItemsSource= webServiceCall;
//
//				Xamarin.Forms.Device.BeginInvokeOnMainThread (()=>{
//					Console.WriteLine ("found size" + webServiceCall.Count);
//				});
//
//			};

			var command = new Command (async () => {
				var webService = new GameRepository ();
				var webServiceCall = await webService.GetGamesAsync ();
				Lv.ItemsSource = webServiceCall;
				Xamarin.Forms.Device.BeginInvokeOnMainThread (() => {
					Console.WriteLine ("found size" + webServiceCall.Count);
				});
			});
		
			getGamesButton.Command = command;

			command.Execute (null);


			Lv.ItemTemplate = new DataTemplate (typeof(TextCell));
			Lv.ItemTemplate.SetBinding (TextCell.TextProperty, "GameName");

			Lv.ItemSelected += (sender, e) => {
				var selectGame = (Game)e.SelectedItem;

				Application.Current.Properties["GameName"] = selectGame.GameName;
				Application.Current.Properties["GamePrice"] = selectGame.Price;
				Application.Current.Properties["GameCount"] = selectGame.InventoryStock;
				Application.Current.Properties["ImageValue"] = Random ();
				Navigation.PushAsync (new GameDetailPage());
			};
			//Console.WriteLine ();

			var layout = new StackLayout { };

			//layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 50});
			layout.Children.Add (wishLabel);
			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 30});
			layout.Children.Add (label);
			layout.Children.Add (getGamesButton);
			layout.Children.Add (Lv);

			Content = layout;
		}

		// Greet User
		public static string GetGreeting ()
		{
			string dashGreeting = "";
			int dateTime = DateTime.Now.Hour;

			if (dateTime >= 0 && dateTime <= 11) {
				dashGreeting = " Good Morning, ";
			} else if (dateTime >= 12 && dateTime <= 17) {
				dashGreeting = " Good Afternoon, ";
			} else if (dateTime >= 18 && dateTime <= 23) {
				dashGreeting = " Good Evening, ";
			} else {
				dashGreeting = "Hello, ";
			}
			return dashGreeting;
		}

		public static int Random(){
			Random randomImage = new Random();
			int value = randomImage.Next(1,7);
			return value;
		}
	}

}

