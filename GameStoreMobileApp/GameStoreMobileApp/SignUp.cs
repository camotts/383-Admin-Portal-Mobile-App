using System;
using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class SignUp:ContentPage
	{
		public SignUp ()
		{
			
			this.Content = new Label 
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Text = "Please Contact our Store Employees to make an account and enjoy " +
					"lots of offers on Games Online and at the Store.",
				FontSize = 25

			};

			// provide the background image
			var backgroundImage = new Image {Aspect = Aspect.Fill };

			backgroundImage.Source =  Device.OnPlatform(
				ImageSource.FromFile("home_background.jpg"),
				ImageSource.FromFile("home_background.jpg"),
				null);


		}
	}
}

