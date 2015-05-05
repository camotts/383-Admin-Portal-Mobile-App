using System;
using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class GameDetailPage:ContentPage
	{
		public GameDetailPage(){

			Content = new ScrollView () {
				Content = new StackLayout () {

					Padding = new Thickness(0,0,0,30),
					Orientation = StackOrientation.Vertical,
					VerticalOptions = LayoutOptions.Start,

					Children = { 
						Picture (),
						Space(),
						GameName(),
						HorzLine (),
						GamePrice (),
						HorzLine (),
						GameCount (),
						HorzLine (),
						Space(),
						GameMenu () 

					}
				}
			}; 
		}

		private Image Picture ()
		{
			
			string rInt = "" + Application.Current.Properties ["ImageValue"]; 

			if (rInt.Contains ("1") || rInt.Contains ("3")  ) {
				return new Image () { 
					Source = "detail1.jpg"
				};
			}
			if (rInt.Contains ("2") || rInt.Contains ("4")) {
				return new Image () { 
					Source = "detail2.jpg"
				};
			}
			else {
				return new Image () { 
					Source = "detail3.jpg"
				};
			}
		}

		private StackLayout GameMenu ()
		{
			return new StackLayout () {

				Padding = 0,
				Orientation = StackOrientation.Horizontal,
				VerticalOptions = LayoutOptions.CenterAndExpand,

				Children = {
					TextButton ("Add To Cart"),
					VertLine (),
					TextButton ("Share"),
					VertLine ()
				}

			};
		}

		private BoxView VertLine ()
		{
			return  new BoxView () {
				Color = Color.FromHex ("ddd"),
				WidthRequest = 1,
				HeightRequest = 40,
				MinimumWidthRequest = 10,
				MinimumHeightRequest = 10,
			};
		}

		private BoxView HorzLine ()
		{
			return  new BoxView () {
				Color = Color.FromHex ("ddd"),
				WidthRequest = 40,
				HeightRequest = 1,
				MinimumWidthRequest = 10,
				MinimumHeightRequest = 10,
			};
		}

		private BoxView Space ()
		{
			return  new BoxView () {
				Color = Color.Transparent,
				WidthRequest = 1,
				HeightRequest = 40,
				MinimumWidthRequest = 10,
				MinimumHeightRequest = 10,
			};
		}

		private Button TextButton (string text)
		{
			return new Button { 
				Text = text,  
				HorizontalOptions = LayoutOptions.CenterAndExpand 
			};
		}

		private Label GameName(){
			return new Label {
				Text = "     "+ Application.Current.Properties ["GameName"],
				FontSize = 20,
				TextColor=Color.Accent
					
			};
		}

		private Label GamePrice(){
			return new Label {
				Text = "         Price :>  $" + Application.Current.Properties ["GamePrice"],
				FontSize = 16,
				TextColor=Color.Accent

			};
		}

		private Label GameCount(){
			return new Label {
				Text = "         Total Left :> " + Application.Current.Properties ["GameCount"],
				FontSize = 16,
				TextColor=Color.Accent

			};
		}




	}


}

