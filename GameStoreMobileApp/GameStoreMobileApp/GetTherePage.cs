using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GameStoreMobileApp
{
	public class GetTherePage : ContentPage
	{
		public GetTherePage(){
		
			Padding = new Thickness (20);
			var outlineFrame = new Frame {

				OutlineColor = Color.Accent,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				BackgroundColor = Color.FromHex ("D#2F2F"),

				Content = new Label {

					XAlign = TextAlignment.Center,
					Text = "FYI !!! \n Built-in Map app will be opened up by leaving the current app.",
					FontSize = 20

				}

			};

			var getDirectionAButton = new Button {
				Text = "Get Location to store A (HQ)",
				BackgroundColor = Color.FromHex ("#FF5722"),
				FontSize = 20,
				TextColor = Color.White
			};

			getDirectionAButton.Clicked += (sender, e) => {

				if(Device.OS == TargetPlatform.iOS){
					Device.OpenUri (new Uri("http://maps.apple.com/?q=100+Janes+Lane+Hammond+LA"));
				} else if(Device.OS == TargetPlatform.Android){
					Device.OpenUri (new Uri("geo:0,0?q=100+Janes+Lane+Hammond+LA"));
				} else if(Device.OS == TargetPlatform.Windows){
					DisplayAlert ("Under Construction","Still working on it. Will be implemented soon..","Ok!");
				}
			};

			var getDirectionBButton = new Button {
				Text = "Get Location to Store B",
				BackgroundColor = Color.FromHex ("#FF5722"),
				FontSize = 20,
				TextColor = Color.White
			};

			getDirectionBButton.Clicked += (sender, e) => {

				if(Device.OS == TargetPlatform.iOS){
					Device.OpenUri (new Uri("http://maps.apple.com/?q=Menlo+Park+CA"));
				} else if(Device.OS == TargetPlatform.Android){
					Device.OpenUri (new Uri("geo:0,0?q=Menlo+Park+CA"));
				} else if(Device.OS == TargetPlatform.Windows){
					DisplayAlert ("Under Construction","Still working on it. Will be implemented soon..","Ok!");
				}
			};

			var getDirectionCButton = new Button {
				Text = "Get Location to Store C",
				BackgroundColor = Color.FromHex ("#FF5722"),
				FontSize = 20,
				TextColor = Color.White
			};

			getDirectionCButton.Clicked += (sender, e) => {

				if(Device.OS == TargetPlatform.iOS){
					Device.OpenUri (new Uri("http://maps.apple.com/?q=Boston+MA"));
				} else if(Device.OS == TargetPlatform.Android){
					Device.OpenUri (new Uri("geo:0,0?q=Boston+MA"));
				} else if(Device.OS == TargetPlatform.Windows){
					DisplayAlert ("Under Construction","Still working on it. Will be implemented soon..","Ok!");
				}
			};

			var layout = new StackLayout ();

			layout.Children.Add (outlineFrame);
			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 29});
			layout.Children.Add(new BoxView() { Color = Color.FromHex ("EEEEEE"), WidthRequest = 100, HeightRequest = 2 });
			layout.Children.Add (getDirectionAButton);
			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 7});
			layout.Children.Add (getDirectionBButton);
			layout.Children.Add (new BoxView {Color = Color.Transparent, HeightRequest = 7});
			layout.Children.Add (getDirectionCButton);

			Content = layout;
		}
	}

}

