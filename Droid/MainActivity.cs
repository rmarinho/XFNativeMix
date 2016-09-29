using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using System.Globalization;
using Xamarin.Forms.Platform.Android;

namespace nativemix.Droid
{
	[Activity(Label = "nativemix.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			var s = Forms.Context;
			LoadApplication(new App());
		}
	}

	public class CustomTextView : Android.Widget.EditText
	{

		public CustomTextView(Context context) : base(context)
		{

		}

		public override void OnEditorAction(Android.Views.InputMethods.ImeAction actionCode)
		{
			base.OnEditorAction(actionCode);
		}

		public override void SetText(Java.Lang.ICharSequence text, TextView.BufferType type)
		{
			base.SetText(text, type);
			if (text != null && text.Length() > 0)
				SetSelection(text.Length());
		}
	}

	public class DroidColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Color)
				return ((Color)value).ToAndroid();

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is global::Android.Graphics.Color)
				return ((global::Android.Graphics.Color)value).ToColor();

			return null;
		}
	}
}
