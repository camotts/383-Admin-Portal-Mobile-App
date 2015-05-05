using System;
using Xamarin.Forms;
using System.Collections.Generic;

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
						Space2 (),
						GameMenu (),TotalGamePicker (),
						AddtoCartButton ()
					
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
					TextButton ("More Info"),
					VertLine (),
					TextButton ("Share")
				}

			};
		}

		private Button AddtoCartButton(){
			var hud = DependencyService.Get<IHud> ();
			var addtoCartButton = new Button { 
				Text = "Add To Cart",
				BackgroundColor = Color.FromHex ("#009688"),
				HorizontalOptions = LayoutOptions.Center,
				TextColor = Color.White,
				WidthRequest = 205
			};

			addtoCartButton.Clicked += (o,e) =>
			{
				hud.ShowSuccessWithStatus ("Added to Cart");

				string previousCartQuantity = ""+Application.Current.Properties["CartQuantity"] ;
				int additionalItem = 1;
				int cartValue=Int32.Parse(previousCartQuantity);
				if(cartValue == 0){
					Application.Current.Properties["CartQuantity"] = "1";
				}
				else{
					additionalItem = additionalItem+1;
					Application.Current.Properties ["CartQuantity"] = additionalItem;
				}
			};

			return addtoCartButton;
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
				HeightRequest = 15,
				MinimumWidthRequest = 10,
				MinimumHeightRequest = 10,
			};
		}

		private BoxView Space2 ()
		{
			return  new BoxView () {
				Color = Color.Transparent,
				WidthRequest = 1,
				HeightRequest = 10,
				MinimumWidthRequest = 10,
				MinimumHeightRequest = 10,
			};
		}
		private Button TextButton (string text)
		{
			return new Button { 
				Text = text,
				TextColor = Color.Black,
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

		private Picker TotalGamePicker(){

			string value = ""+ Application.Current.Properties ["GameCount"];
			int finalValue = Int32.Parse(value);
			int[] gameQuantity;
			gameQuantity = new int[finalValue];

			var picker = new Picker
			{

				Title = "Quantity",
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			Padding = new Thickness (35, 0, 35, 0);

			picker.SelectedIndexChanged += (sender, args) =>
			{
				if (picker.SelectedIndex == -1)
				{
					Application.Current.Properties["CartQuantity"] = '0';
				}
				else
				{
					Application.Current.Properties["CartQuantity"] = picker.Items[picker.SelectedIndex];


				}
			};

			return picker;
		}




	}


}

