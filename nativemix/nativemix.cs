using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace nativemix
{
	public class App : Application
	{
		public App()
		{
			var vm = new DemoViewModel { LabelName = "Hello from Forms" };
			vm.ChangeTextCommand = new Command(() => { vm.LabelName = "Changed Text from Froms"; });
			MainPage = new NavigationPage(new MyPage { Title = "NativeVies", BindingContext = vm });
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}

	public class DemoViewModel : INotifyPropertyChanged
	{

		public Command ChangeTextCommand
		{
			get;
			set;
		}

		string _labelName;
		public string LabelName
		{
			get { return _labelName; }
			set
			{
				if (_labelName == value)
					return;
				_labelName = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
