using System;
using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class UnauthorizedLookAround:ContentPage
	{
		public UnauthorizedLookAround(){
			this.Title="For Unauthorized Users";


			var label = new Label { 
				Text = "The shitiest place you can be in! Trust me."
			};

			var layout = new StackLayout ();

			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 50});
			layout.Children.Add (label);

			Content = layout;
		}
	}

}

