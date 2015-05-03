using System;
using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class LookAround: TabbedPage
	{
		public LookAround ()
		{
			var hud = DependencyService.Get<IHud> ();

			this.Title = "Store";

			hud.Show("Loading Store");
			Device.StartTimer (new TimeSpan (0, 0, 4), () => {
				hud.Dismiss ();
				return false; // runs again, or false to stop
			});
				
			Children.Add (new AllGames {Title="All",Icon="Menu-32.png"});
			Children.Add (new BestSellingGames{Title="Best Seller",Icon="Star-32.png"});
			Children.Add (new SearchGame {Title="Search",Icon="Search-32.png"});
		}


	}
}

