using System;
using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class CheckOutPage:ContentPage
	{
		public CheckOutPage(){

			this.Title="Check Out";
			this.BackgroundImage = "33.jpg";

			var label = new Label { 
				Text = "Total items in Cart: " + Application.Current.Properties["CartQuantity"],
				FontSize = 30,
				HorizontalOptions = LayoutOptions.Center
			};

			var layout = new StackLayout ();
			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 60});
			layout.Children.Add (label);

			Content = layout;
		}


	}


}

