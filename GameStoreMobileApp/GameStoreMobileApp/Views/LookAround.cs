using System;
using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class LookAround: ContentPage
	{
		public LookAround ()
		{
			var hud = DependencyService.Get<IHud> ();

			this.Title = "Store";

			hud.Show("Loading Store");

			var label = new Label { 
				Text = "This is after user Logs IN."  
			};

			var layout = new StackLayout ();

			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 50});
			layout.Children.Add (label);

			Content = layout;
		}


	}
}

