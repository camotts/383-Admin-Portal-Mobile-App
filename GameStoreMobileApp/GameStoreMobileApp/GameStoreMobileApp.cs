using System;

using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class App : Application
	{
		public App ()
		{
			var headingLabel = new MyLabel {
				XAlign = TextAlignment.Center,
				Text = "Best Game Store",
				FontFamily = Device.OnPlatform (
					"Money Money",
					"BEAUTYSC",
					null),
				FontSize = 55
					
			};

			// The root page of your application
			MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {headingLabel}
					}
			};

		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

