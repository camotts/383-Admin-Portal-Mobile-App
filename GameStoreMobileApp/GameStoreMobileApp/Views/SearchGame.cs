using System;
using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class SearchGame:ContentPage
	{
		Label resultsLabel;
		public SearchGame(){
			this.BackgroundImage = "33.jpg";


			SearchBar searchBar = new SearchBar
			{
				Placeholder = "Serach Game",
			};

			var layout = new StackLayout ();
			//searchBar.SearchButtonPressed += OnSearchBarButtonPressed;
			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 20});
			layout.Children.Add (searchBar);

			Content = layout;
		}
	}

}

