using System;
using Xamarin.Forms;

namespace GameStoreMobileApp
{
	public class UserAccount:ContentPage
	{
		public UserAccount ()
		{
			// Define command for the items in the TableView.
			Command<Type> navigateCommand = 
				new Command<Type>(async (Type pageType) =>
					{
						Page page = (Page)Activator.CreateInstance(pageType);
						await this.Navigation.PushAsync(page);
					});

			this.Title = "Account";
			this.Content = new TableView
			{
				Intent = TableIntent.Menu,
				Root = new TableRoot
				{
					new TableSection("Store Options")
					{
						new TextCell
						{
							Text = "Account Balance",
							Command = navigateCommand,
							CommandParameter = typeof(AccountBalance)
						}
					},

					new TableSection("")
					{
						new TextCell
						{
							Text = "Order History",
							Command = navigateCommand,
							CommandParameter = typeof(OrderHistory)
						},

						new TextCell
						{
							Text = "Account Settings",
							Command = navigateCommand,
							CommandParameter = typeof(AccountSettings)
						},
						new TextCell
						{
							Text = "Billing Info",
							Command = navigateCommand,
							CommandParameter = typeof(BillingInfo)
						},
						new TextCell
						{
							Text = "Shipping Info",
							Command = navigateCommand,
							CommandParameter = typeof(ShippingInfo)
						},
						new TextCell
						{
							Text = "Redeem Discount",
							Command = navigateCommand,
							CommandParameter = typeof(RedeemDiscount)
						},
						new TextCell
						{
							Text = "Help",
							Command = navigateCommand,
							CommandParameter = typeof(Help)
						}
					}, 

					new TableSection("About Anfield")
					{
						new TextCell
						{
							Text = "Notifications",
							Command = navigateCommand,
							CommandParameter = typeof(Notofications)
						},

						new TextCell
						{
							Text = "Legal",
							Command = navigateCommand,
							CommandParameter = typeof(Legal)
						}
					},
					new TableSection("")
					{
						new TextCell
						{
							Text = "LOG OUT",
							Command = navigateCommand,
							CommandParameter = typeof(LOGOUT)
						}
					}

				}
			};
		}

	}
}

