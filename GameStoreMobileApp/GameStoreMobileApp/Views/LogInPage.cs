using System;
using Xamarin.Forms;
using System.Threading.Tasks;


namespace GameStoreMobileApp
{
	
	public class LogInPage: ContentPage
	{
		public LogInPage ()
		{
			var hud = DependencyService.Get<IHud> ();
			this.Title = "Log In";
			Padding = new Thickness (20);


			Label header = new Label {
				Text = "Log In with Your Email",
				FontSize = 27,
				HorizontalOptions = LayoutOptions.Center
			};

			Entry loginInput = new Entry {
				Keyboard = Keyboard.Email,
				Placeholder = "Email Address",
				VerticalOptions = LayoutOptions.Center,

			};
				
			loginInput.SetBinding (Entry.TextProperty, "Email");

			Entry passwordInput = new Entry {
				
				Keyboard = Keyboard.Text,
				IsPassword = true, 
				Placeholder = "Password",
				VerticalOptions = LayoutOptions.Center
			};

			loginInput.SetBinding (Entry.TextProperty, "Password");

			var LogInButton = new Button {
				Text = "Log In",
				TextColor = Color.White,
				BackgroundColor = Color.FromHex ("#4FC3F7"),
				Font = Font.SystemFontOfSize (20),
				WidthRequest = 10,
				HeightRequest = 40,


			};


			LogInButton.Clicked += (sender, ea) => {
				new UserRepository ().OnLogInButtonClicked (loginInput.Text, passwordInput.Text);
			
					
					
				if (new UserRepository ().OnLogInButtonClicked (loginInput.Text, passwordInput.Text) == true) {

					Device.StartTimer (new TimeSpan (0, 0, 3), () => {
						hud.ShowSuccessWithStatus("Verified!! Welcome to Anfiled!");
						return false; // runs again, or false to stop
					});

					Device.StartTimer (new TimeSpan (0, 0, 5), () => {
						Navigation.PushModalAsync (new LookAround ());
						return false; // runs again, or false to stop
					});

					Device.StartTimer (new TimeSpan (0, 0, 8), () => {
						hud.Dismiss ();
						return false; // runs again, or false to stop
					});
						
				} else {
					hud.ShowErrorWithStatus ("Invalid Credentials");
					DisplayAlert ("Sorry! Invalid Credentials.", "Please Check your Email or Password!", "Try Again");
				}
			};



			Label noAccount = new Label {
				Text = "Don't have an account?",
				FontSize = 13,
				FontAttributes = FontAttributes.Bold,
				HorizontalOptions = LayoutOptions.Center
			};

			var SignUpButton = new Button {
				Text = "Sign Up",
				TextColor = Color.FromHex ("#FF5722"),
				//BackgroundColor = Color.FromHex ("#FF5722"),
				Font = Font.SystemFontOfSize (20),
				WidthRequest = 1,
				HeightRequest = 40,
				BackgroundColor = Color.Transparent

			};

			SignUpButton.Clicked += (o, e) => {
				Navigation.PushAsync (new SignUp ());

			};

			if (Device.OS == TargetPlatform.iOS) {
				this.BackgroundImage = "17.jpg";
			} else if (Device.OS == TargetPlatform.Android) {
				this.BackgroundImage = "drawable/seventeen.jpg";
			} else if (Device.OS == TargetPlatform.Windows) {
				DisplayAlert ("Under Construction", "Still working on it. Will be implemented soon..", "Ok!");
			}

			var layout = new StackLayout ();

			layout.Children.Add (new BoxView { Color = Color.Transparent, HeightRequest = 35 });
			layout.Children.Add (header);
			layout.Children.Add (new BoxView { Color = Color.Transparent, HeightRequest = 45 });
			layout.Children.Add (loginInput);
			layout.Children.Add (new BoxView { Color = Color.Transparent, HeightRequest = 5 });
			layout.Children.Add (passwordInput);
			layout.Children.Add (LogInButton);
			layout.Children.Add (new BoxView { Color = Color.Transparent, HeightRequest = 5 });
			layout.Children.Add (noAccount);
			layout.Children.Add (new BoxView { Color = Color.Transparent, HeightRequest = 1 });
			layout.Children.Add (SignUpButton);


			ScrollView scroll = new ScrollView {
				Content = layout 
					
			};

			Content = scroll;
		}
	}
}

