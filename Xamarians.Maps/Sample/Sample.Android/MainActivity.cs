using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Xamarians.Maps;

namespace Sample.Droid
{
	[Activity (Label = "Sample", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;            
            base.OnCreate (bundle);
            Forms.Init (this, bundle);
            Xamarians.Maps.Droid.ExtendedMapRenderer.Init();
            LoadApplication (new Sample.App ());
            //var tt = PackageName;
            //int resID = Resources.GetIdentifier("Icon.png", "drawable", "Sample.Android");
        }
	}
}

