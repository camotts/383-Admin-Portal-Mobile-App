using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GameStoreMobileApp
{
	public class StoreMapPage : ContentPage
	{
		public StoreMapPage(){
			var map = new Map (
				         MapSpan.FromCenterAndRadius (
					         new Position (30.0219504, -89.8830829), Distance.FromMiles (0.3))) {
				IsShowingUser = true,
				HeightRequest = 100,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
					
			// pin first address
			var positionA = new Position (30.517174,-90.463322);

			var pinA = new Pin {
				Type = PinType.Place,
				Position = positionA,
				Label = "Store A (HQ)",
				Address = "100 Janes Lane, \n Hammond, LA, \n 70401"
			};
			map.Pins.Add (pinA);

			// pin second address
			var positionB = new Position (37.42565,-122.13535);

			var pinB = new Pin {
				Type = PinType.Place,
				Position = positionB,
				Label = "Store B",
				Address = "Silicon Valley, Palo Alto, CA, \n 94025"
			};
			map.Pins.Add (pinB);


			// pin third address
			var positionC = new Position (42.360091,-71.09416);

			var pinC = new Pin {
				Type = PinType.Place,
				Position = positionC,
				Label = "Store C",
				Address = "Boston, MA, \n 02481"
			};
			map.Pins.Add (pinC);


			// add the slider
			var slider = new Slider (1, 18, 1);
			slider.ValueChanged += (sender, e) => {
				var zoomLevel = e.NewValue; // between 1 and 18
				var latlongdegrees = 360 / (Math.Pow (2, zoomLevel));
				//Debug.WriteLine(zoomLevel + " -> " + latlongdegrees);
				if (map.VisibleRegion != null)
					map.MoveToRegion (new MapSpan (map.VisibleRegion.Center, latlongdegrees, latlongdegrees)); 
			};

			var stackLayout = new StackLayout { Spacing = 0 };
			stackLayout.Children.Add (map);
			stackLayout.Children.Add (slider);

			Content = stackLayout;
		}
	}

}

