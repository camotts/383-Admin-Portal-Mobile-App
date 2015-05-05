using System;
using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class LOGOUT:ContentPage
	{
		public LOGOUT(){

			Padding = new Thickness (30);
			Title = "Log Out";
			var LogOutButton = new Button {
				Text = "Log Out",
				TextColor = Color.White,
				BackgroundColor = Color.Red,
				Font = Font.SystemFontOfSize( 20 ),
				WidthRequest = 05,
				HeightRequest = 40

			};

			var label = new Label{ 
				Text = "Anfiled v0.9",
				FontSize = 12,
				HorizontalOptions = LayoutOptions.Center
					
			};

			var hud = DependencyService.Get<IHud> ();
			LogOutButton.Clicked += (o,e) => 
			{
				hud.ShowSuccessWithStatus("Logged Out!");
				Device.StartTimer (new TimeSpan (0, 0, 2), () => {
					Navigation.PushModalAsync (new HomeScreen ());
					return false; // runs again, or false to stop
				});

				Application.Current.Properties.Clear ();
			};


			var layout = new StackLayout ();
			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 50});
			layout.Children.Add (LogOutButton);
			layout.Children.Add (label);

			Content = layout;
		}
	}

}

