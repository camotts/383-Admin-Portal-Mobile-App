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
				Text = "Best Game Store",
				FontFamily = Device.OnPlatform (
					"Money Money",
					"BEAUTYSC",
					null),
				FontSize = 55

			};

			var layout = new StackLayout();

			layout.Children.Add (headingLabel);

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

