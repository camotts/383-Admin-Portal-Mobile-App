using System;
using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class HomeScreen : ContentPage
	{
		public HomeScreen ()
		{
			// provide the heading label
			var headingLabel = new MyLabel {
				XAlign = TextAlignment.Center,
				YAlign = TextAlignment.Center,
				Text = "GameStore",
				FontFamily = Device.OnPlatform (
					"Money Money",
					"Money Money",
					null),
				FontSize = Device.OnPlatform (42,45,99)

			};

			var button = new Button {
				Text = "Sign In",
				TextColor = Color.White,
				BackgroundColor = Color.FromHex ("#2196F3"),
				Font = Font.SystemFontOfSize( 20 ),
				WidthRequest = 10,
				HeightRequest = 40,


			};

			var layout = new StackLayout();

			if(Device.OS == TargetPlatform.iOS) {
				layout.Padding = new Thickness (35,2,0,2);

			}
			if (Device.OS == TargetPlatform.Android) {
				layout.Padding = new Thickness (35, 2, 0, 2);
				headingLabel.TextColor = Color.Black;
			} 
			else {
				layout.Padding = new Thickness (35, 2, 0, 2);
			}

			layout.Children.Add (headingLabel);
			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 90});
			layout.Children.Add (button);

			// provide the background image
			var homeScreenImage = new Image {Aspect = Aspect.Fill };

			homeScreenImage.Source =  Device.OnPlatform(
				ImageSource.FromFile("home_background.jpg"),
				ImageSource.FromFile("home_background.jpg"),
				null);


			// merge views and create a layout
			var relativeLayout = new RelativeLayout ();

			relativeLayout.Children.Add(homeScreenImage,
				Constraint.Constant(0),
				Constraint.Constant(0),
				Constraint.RelativeToParent((parent) => parent.Width),
				Constraint.RelativeToParent((parent) => parent.Height));

			relativeLayout.Children.Add(layout,
				Constraint.Constant(0),
				Constraint.RelativeToParent((parent) => {return parent.Width/2;} ),
				Constraint.RelativeToParent((parent) => {return parent.Height/2;} ));

			 
			Content = relativeLayout;
			
		}
	}
}

