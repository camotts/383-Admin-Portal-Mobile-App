using System;
using BigTed;
using Xamarin.Forms;
using GameStoreMobileApp.iOS;

[assembly: Dependency(typeof(HudService))]
namespace GameStoreMobileApp.iOS
{
	
	public class HudService : IHud
	{
		public HudService ()
		{
		}

		public void Show() {
			BTProgressHUD.Show ();
		}

		public void Show(string message) {
			BTProgressHUD.Show (message);
		}

		public void ShowSuccessWithStatus(string message){
			BTProgressHUD.ShowSuccessWithStatus (message);
		}

		public void ShowErrorWithStatus(string message){
			BTProgressHUD.ShowErrorWithStatus (message);
		}

		public void Dismiss() {
			BTProgressHUD.Dismiss ();
		}
	}
}

