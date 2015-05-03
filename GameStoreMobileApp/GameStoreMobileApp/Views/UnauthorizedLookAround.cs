using System;
using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class UnauthorizedLookAround : ContentPage
	{
		int tapCount;
		//readonly Label label;

		public UnauthorizedLookAround(){
			this.Title="Store";

			var hud = DependencyService.Get<IHud> ();
			hud.Show ();

			Device.StartTimer (new TimeSpan (0, 0, 3), () => {
				hud.Dismiss ();
				return false; // runs again, or false to stop
			});

			Grid grid = new Grid {
				RowSpacing = 0,VerticalOptions = LayoutOptions.Fill,
				RowDefinitions =
				{
					new RowDefinition(){ Height = new GridLength(180,GridUnitType.Absolute) },
					new RowDefinition(){ Height = new GridLength(180,GridUnitType.Absolute) },
					new RowDefinition(){ Height = new GridLength(200,GridUnitType.Absolute) },
					new RowDefinition(){ Height = new GridLength(180,GridUnitType.Absolute) },
					new RowDefinition(){ Height = new GridLength(180,GridUnitType.Absolute) },
					new RowDefinition(){ Height = new GridLength(50,GridUnitType.Absolute) },
				}

			};
	
			var LogInButton = new Button {
				Text = "Log In",
				TextColor = Color.White,
				BackgroundColor = Color.Accent,
				Font = Font.SystemFontOfSize(20),
				WidthRequest = 9,
				HeightRequest = 40,

			};

			var firstImage = new Image { Aspect = Aspect.AspectFit };
			firstImage.Source = ImageSource.FromFile("ad1.jpg");

			var secondImage = new Image { Aspect = Aspect.AspectFit };
			secondImage.Source = ImageSource.FromFile("ad5.jpg");

			var thirdImage = new Image { Aspect = Aspect.AspectFit };
			thirdImage.Source = ImageSource.FromFile("ad2.jpg");

			var fourthImage = new Image { Aspect = Aspect.AspectFit };
			fourthImage.Source = ImageSource.FromFile("ad4.jpg");

			var fifthImage = new Image { Aspect = Aspect.AspectFit };
			fifthImage.Source = ImageSource.FromFile("ad3.jpg");

			//grid.Children.Add (LogInButton);
			grid.Children.Add (firstImage);
			grid.Children.Add (secondImage, 0,1);
			grid.Children.Add (thirdImage, 0,2);
			grid.Children.Add (fourthImage, 0, 3);
			grid.Children.Add (fifthImage, 0, 4);
			grid.Children.Add (LogInButton,0,5);


			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += OnTapGestureRecognizerTapped;

			firstImage.GestureRecognizers.Add(tapGestureRecognizer);
			secondImage.GestureRecognizers.Add(tapGestureRecognizer);
			thirdImage.GestureRecognizers.Add(tapGestureRecognizer);
			fourthImage.GestureRecognizers.Add(tapGestureRecognizer);
			fifthImage.GestureRecognizers.Add(tapGestureRecognizer);

			LogInButton.Clicked += (o,e) =>
			{Navigation.PushAsync (new LogInPage());};
				
			ScrollView scroll = new ScrollView {
				VerticalOptions = LayoutOptions.FillAndExpand, 
				IsClippedToBounds = true
			};

			scroll.Content = grid;

			Content = scroll;
		}


		// when the images are tapped
		void OnTapGestureRecognizerTapped(object sender, EventArgs args)
		{
			var hud = DependencyService.Get<IHud> ();
			tapCount++;

			//var imageSender = (Image)sender;

			if (tapCount % 1 == 0) {
				hud.ShowErrorWithStatus ("Please Log In for Details");

			} else {
				hud.ShowErrorWithStatus ("Please Log In BRO .");
			}
			//Console.WriteLine ("image tapped: " + ((FileImageSource)imageSender.Source).File);
		}
	}

}

