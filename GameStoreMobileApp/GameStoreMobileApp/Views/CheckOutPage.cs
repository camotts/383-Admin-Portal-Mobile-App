using System;
using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class CheckOutPage:ContentPage
	{
		public CheckOutPage(){

			this.Title="Check Out";
			this.BackgroundImage = "33.jpg";
		
			string showText = "";

			if (CheckCart () == true) {
				showText = "Total items in Cart: " + Application.Current.Properties ["CartQuantity"];
			} else {
				showText="Please buy something to add it to the Cart.";
			}

			var label = new Label { 
				Text = showText,
				FontSize = 25,
				HorizontalOptions = LayoutOptions.Center
			};

			var BuyGame = new Button {
				Text = "Check Out",
				TextColor = Color.White,
				BackgroundColor = Color.FromHex ("#2196F3"),
				Font = Font.SystemFontOfSize( 20 ),
				WidthRequest = 145,
				HeightRequest = 40,
				HorizontalOptions= LayoutOptions.Center

			};

			BuyGame.Clicked += (o,e) => 
			{
				DisplayAlert ("Check Out!", "We are still in BETA! Adding it SOON!!!", "OK!");
			};

			var layout = new StackLayout ();
			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 60});
			layout.Children.Add (label);
			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 40});
			layout.Children.Add (BuyGame);

			Content = layout;
		}

		public static Boolean CheckCart(){
			string previousCartQuantity = ""+Application.Current.Properties["CartQuantity"] ;
			int cartValue=Int32.Parse(previousCartQuantity);
			if (cartValue == 0) {
				return false;
			} else {
				return true;
			}
		}
	}


}

