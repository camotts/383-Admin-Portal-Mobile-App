using System;
using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class HomeScreen : ContentPage
	{
		public HomeScreen ()
		{
			this.Title = " Best Store in Town";
			NavigationPage.SetBackButtonTitle (this, "Home");

			// provide the heading label
			var headingLabel = new MyLabel {
				XAlign = TextAlignment.Center,
				YAlign = TextAlignment.Center,
				Text = "ANFIELD",
				FontFamily = Device.OnPlatform (
					"Money Money",
					"Money Money",
					null),
				FontSize = Device.OnPlatform (42,45,99)

			};

			var SignUpButton = new Button {
				Text = "Sign In",
				TextColor = Color.White,
				BackgroundColor = Color.FromHex ("#2196F3"),
				Font = Font.SystemFontOfSize( 20 ),
				WidthRequest = 05,
				HeightRequest = 40


			};

			SignUpButton.Clicked += (o,e) => 
			{Navigation.PushAsync (new SignUp());};


			var layout = new StackLayout();

//			if(Device.OS == TargetPlatform.iOS) {
//				layout.Padding = new Thickness (20,2,0,2);
//
//			}
//			if (Device.OS == TargetPlatform.Android) {
//				layout.Padding = new Thickness (35, 2, 0, 2);
//				headingLabel.TextColor = Color.Black;
//			} 
//			else {
//				layout.Padding = new Thickness (35, 2, 0, 2);
//			}

			layout.Children.Add (headingLabel);
			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 90});
			layout.Children.Add (SignUpButton);

			// provide the background image
			var homeScreenImage = new Image {Aspect = Aspect.Fill };

			homeScreenImage.Source =  Device.OnPlatform(
				ImageSource.FromFile("home_background.jpg"),
				ImageSource.FromFile("home_background.jpg"),
				null);


			// merge views and create a layout
			var relativeLayout = new RelativeLayout ();

			relativeLayout.Children.Add(homeScreenImage,
				Constraint.RelativeToParent (
					((parent)=>{return 0;})
				));

			relativeLayout.Children.Add(layout,
				Constraint.RelativeToParent((parent) => {return parent.Width/8;} ),
				Constraint.RelativeToParent((parent) => {return parent.Height/3;} ));

			 
			Content = relativeLayout;
			
		}
	}
}

