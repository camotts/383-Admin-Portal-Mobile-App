using System;

namespace GameStoreMobileApp
{
	public interface IHud
	{
		void Show();
		void Show(string message);
		void ShowSuccessWithStatus (string message);
		void ShowErrorWithStatus (string message);
		void Dismiss();
	}
}

