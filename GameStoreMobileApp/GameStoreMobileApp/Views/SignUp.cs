using System;
using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class SignUp:ContentPage
	{
		public SignUp ()
		{
			this.Title = "Sign Up";
			NavigationPage.SetBackButtonTitle (this, "Sign Up");

			Padding = new Thickness (20);
			var outlineFrame = new Frame {

				OutlineColor = Color.Accent,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				BackgroundColor = Color.FromHex ("D#2F2F"),

				Content = new Label {

					XAlign = TextAlignment.Center,
					Text = "Please Contact our Store Employees to make a Free Account and Enjoy " +
						"lots of Offers On Games Online and at the Stores.",
					FontSize = 20

				}

			};

			var ContactUsButton = new Button {
				Text = "Contact Us",
				TextColor = Color.White,
				BackgroundColor = Color.FromHex ("#FF5722"),
				Font = Font.SystemFontOfSize( 20 ),
				WidthRequest = 1,
				HeightRequest = 40

			};

			var LocateUsButton = new Button {
				Text = "Locate Our Stores",
				TextColor = Color.White,
				BackgroundColor = Color.FromHex ("#FF5722"),
				Font = Font.SystemFontOfSize( 20 ),
				WidthRequest = 1,
				HeightRequest = 40

			};

			ContactUsButton.Clicked += (o,e) => 
			{Navigation.PushAsync (new ContactUs());};
		
			LocateUsButton.Clicked += (o,e) => 
			{Navigation.PushAsync (new LocateUs());};


			var layout = new StackLayout();

			layout.Children.Add (outlineFrame);
			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 45});
			layout.Children.Add (ContactUsButton);
			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 25});
			layout.Children.Add (LocateUsButton);

			layout.Padding = new Thickness (Device.OnPlatform (5, 0, 0), Device.OnPlatform (45, 0, 0), Device.OnPlatform (5, 0, 0), Device.OnPlatform (5, 0, 0));

			// merge views and create a layout
			var relativeLayout = new RelativeLayout ();

			//relativeLayout.BackgroundColor = Color.FromHex ("FAFAFA");

			relativeLayout.Children.Add(layout,
				Constraint.RelativeToParent((parent) => {return parent.Width/50;} ),
				Constraint.RelativeToParent((parent) => {return parent.Height/10;} ));
			

			Content = relativeLayout;
		}
	}
}

