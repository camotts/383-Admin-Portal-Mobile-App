using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GameStoreMobileApp
{
	public class LocateUs : TabbedPage
	{
		public LocateUs ()
		{
			this.Title = "Locate Us";

			Children.Add (new StoreMapPage {Title="Our Stores",Icon="glypish_globe.png"});
			Children.Add (new GetTherePage {Title="Get There",Icon="glypish_way.png"});
		
		}
	}
}

