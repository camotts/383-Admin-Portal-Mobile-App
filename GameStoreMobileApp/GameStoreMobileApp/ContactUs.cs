using System;
using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class ContactUs:ContentPage
	{
		public ContactUs ()
		{
			this.Title = "Contact Us";
			NavigationPage.SetBackButtonTitle (this, "Contact Us");

			var absoluteLayout = new AbsoluteLayout {
				Padding = new Thickness(20)
			};

			// for the Our Location Title
			absoluteLayout.Children.Add(
				new BoxView
				{
					Color = Color.Accent
				},
				new Rectangle(0,10,200,5)
			);

			absoluteLayout.Children.Add(
				new BoxView
				{
					Color = Color.Accent
				},
				new Rectangle(0,20,200,5)
			);

			absoluteLayout.Children.Add(
				new BoxView
				{
					Color = Color.Accent
				},
				new Rectangle(10,0,5,65)
			);

			absoluteLayout.Children.Add(
				new BoxView
				{
					Color = Color.Accent
				},
				new Rectangle(20,0,5,65)
			);

			absoluteLayout.Children.Add (
				new Label
				{
					Text = "Our Location",
					FontSize = 28
				},
				new Point(30,25)
			);

			absoluteLayout.Children.Add (
				new Label
				{
					Text = "100 Janes lane, Hammond" + ", Louisiana, 70401, USA"
				},
				new Point (0,80)
			);
		
			this.Content = absoluteLayout;
		}


	}
}

