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
					Color = Color.FromHex ("#FF5722")
				},
				new Rectangle(0,20,200,5)
			);

			absoluteLayout.Children.Add(
				new BoxView
				{
					Color = Color.FromHex ("#FF5722")
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
					FormattedText = new FormattedString{
						Spans = {
							new Span {
								Text = "Head Office:",
								FontAttributes = FontAttributes.Bold
							},
							new Span {
								Text = "\n100 Janes Lane, Hammond" + "\nLouisiana, 70401 \nUSA"
							}
						}
					}
				},
				new Point (0,80)
			);
		
			// creating a separator
			absoluteLayout.Children.Add(
				new BoxView
				{
					Color = Color.FromHex ("#039BE5")
				},
				new Rectangle(0,170,275,2)
			);

			// for the By Email Title
			absoluteLayout.Children.Add(
				new BoxView
				{
					Color = Color.Accent
				},
				new Rectangle(0,200,200,5)
			);

			absoluteLayout.Children.Add(
				new BoxView
				{
					Color = Color.FromHex ("#FF5722")
				},
				new Rectangle(0,210,200,5)
			);

			absoluteLayout.Children.Add(
				new BoxView
				{
					Color = Color.FromHex ("#FF5722")
				},
				new Rectangle(10,190,5,65)
			);

			absoluteLayout.Children.Add(
				new BoxView
				{
					Color = Color.Accent
				},
				new Rectangle(20,190,5,65)
			);

			absoluteLayout.Children.Add (
				new Label
				{
					Text = "By Email",
					FontSize = 28
				},
				new Point(30,215)
			);

			absoluteLayout.Children.Add (
				new Label
				{
					Text = "inquiry@anfield.com"
				},
				new Point (0,267)
			);

			// creating a separator
			absoluteLayout.Children.Add(
				new BoxView
				{
					Color = Color.FromHex ("#039BE5")
				},
				new Rectangle(0,300,275,2)
			);

			// for the By Phone Title
			absoluteLayout.Children.Add(
				new BoxView
				{
					Color = Color.Accent
				},
				new Rectangle(0,330,200,5)
			);

			absoluteLayout.Children.Add(
				new BoxView
				{
					Color = Color.FromHex ("#FF5722")
				},
				new Rectangle(0,340,200,5)
			);

			absoluteLayout.Children.Add(
				new BoxView
				{
					Color = Color.FromHex ("#FF5722")
				},
				new Rectangle(10,320,5,65)
			);

			absoluteLayout.Children.Add(
				new BoxView
				{
					Color = Color.Accent
				},
				new Rectangle(20,320,5,65)
			);

			absoluteLayout.Children.Add (
				new Label
				{
					Text = "By Phone",
					FontSize = 28
				},
				new Point(30,345)
			);

			absoluteLayout.Children.Add (
				new Label
				{
					FormattedText = new FormattedString{
						Spans = {
							new Span {
								Text = "Customer Service:",
								FontAttributes = FontAttributes.Bold
							},
							new Span {
								Text = " 985-549-2222"
							},
							new Span {
								Text = "\nShippings and Returns:",
								FontAttributes = FontAttributes.Bold
							},
							new Span {
								Text = " 911"
							}
						}
					}
				},
				new Point (0,390)
			);




			this.Content = absoluteLayout;
		}


	}
}

