using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Widget;
using Android.Graphics;
using GameStoreMobileApp;
using GameStoreMobileApp.Droid;


[assembly: ExportRenderer (typeof(MyLabel), typeof(MyLabelRendererDroid))]
namespace GameStoreMobileApp.Droid
{
	public class MyLabelRendererDroid : LabelRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Label> e){

			base.OnElementChanged (e);

			var label = (TextView)Control;
			Typeface font = Typeface.CreateFromAsset (Forms.Context.Assets, "Money Money.ttf");
			label.Typeface = font;
		}
	}
}

