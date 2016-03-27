using System;
using MvvmCross.Droid.Views;
using Android.App;

namespace CDLXamarinTest.Droid.Views
{
	[Activity(Label = "Movie List")]
	public class ListView : MvxActivity
	{
		protected override void OnCreate (Android.OS.Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ListView);
		}
	}
}

