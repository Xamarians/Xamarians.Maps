using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Sample
{
	public partial class MyPage : ContentPage
	{
		public MyPage()
		{
			InitializeComponent();
		}
		private void Handle_Clicked(object sender, EventArgs ex)
		{
			Navigation.PushAsync(new NavigationPage(new TestCircle()));
		}
	}
}
