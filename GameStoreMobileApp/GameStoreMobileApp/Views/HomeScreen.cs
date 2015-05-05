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
					"Exo2-ExtraBold",
					"Money Money",
					null),
				FontSize = Device.OnPlatform (54,45,99),
				TextColor = Color.FromHex ("#D84315")

			};

			var SignUpButton = new Button {
				Text = "Sign Up",
				TextColor = Color.White,
				BackgroundColor = Color.Accent,
				Font = Font.SystemFontOfSize( 20 ),
				WidthRequest = 05,
				HeightRequest = 40


			};

			var LogInButton = new Button {
				Text = "Log In",
				TextColor = Color.White,
				BackgroundColor = Color.FromHex ("#2196F3"),
				Font = Font.SystemFontOfSize( 20 ),
				WidthRequest = 05,
				HeightRequest = 40,

			
			};

			var LookAroundButton = new Button {
				Text = "Look Around",
				TextColor = Color.White,
				BackgroundColor = Color.FromHex ("#2196F3"),
				Font = Font.SystemFontOfSize( 20 ),
				WidthRequest = 05,
				HeightRequest = 40
			
			};



			SignUpButton.Clicked += (o,e) => 
			{Navigation.PushAsync (new SignUp());};

			LogInButton.Clicked += (o,e) =>
			{Navigation.PushAsync (new LogInPage());};

			LookAroundButton.Clicked += (o,e) =>
			{
//				if(Application.Current.Properties.ContainsKey("UserId")){
//					Navigation.PushAsync (new NavigationPage(new LookAround()));
//				}
//				else{
					Navigation.PushAsync (new UnauthorizedLookAround());
				//}
			};
				

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

			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 50});
			layout.Children.Add (headingLabel);
			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 50});
			layout.Children.Add (SignUpButton);
			
			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 20});
			layout.Children.Add(new BoxView() { Color = Color.FromHex ("EEEEEE"), WidthRequest = 100, HeightRequest = 2 });
			layout.Children.Add (LogInButton);
			layout.Children.Add (LookAroundButton);

			// provide the background image
			var homeScreenImage = new Image {Aspect = Aspect.Fill };

			homeScreenImage.Source =  Device.OnPlatform(
				ImageSource.FromFile("21.jpg"),
				ImageSource.FromFile("home.jpg"),
				null);

			// merge views and create a layout
			var relativeLayout = new RelativeLayout ();

			relativeLayout.Children.Add(homeScreenImage,
				Constraint.RelativeToParent (
					((parent)=>{return 0;})
				));

			relativeLayout.Children.Add(layout,
				Constraint.RelativeToParent((parent) => {return parent.Width/6.5;} ),
				Constraint.RelativeToParent((parent) => {return parent.Height/4;} ));


			Content = relativeLayout;
			
		}
	}
}

