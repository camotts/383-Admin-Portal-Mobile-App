using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GameStoreMobileApp
{
	public class LocateUs : ContentPage
	{
		public LocateUs ()
		{
			var map = new Map(
				MapSpan.FromCenterAndRadius(
					new Position(37,-122), Distance.FromMiles(0.3))) {
				IsShowingUser = true,
				HeightRequest = 100,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			// add the slider
			var slider = new Slider (1, 18, 1);
			slider.ValueChanged += (sender, e) => {
				var zoomLevel = e.NewValue; // between 1 and 18
				var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
				//Debug.WriteLine(zoomLevel + " -> " + latlongdegrees);
				if (map.VisibleRegion != null)
					map.MoveToRegion(new MapSpan (map.VisibleRegion.Center, latlongdegrees, latlongdegrees)); 
			};

			var stackLayout = new StackLayout { Spacing = 0 };
			stackLayout.Children.Add(map);
			stackLayout.Children.Add (slider);

			Content = stackLayout;
		}
	}
}

