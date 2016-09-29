using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace nativemix
{
	public partial class MyPage : ContentPage
	{
		public MyPage()
		{
			InitializeComponent();
			BindingContext = new DemoViewModel();
		}
	}

	public class DemoViewModel : INotifyPropertyChanged
	{
		public Command ChangeTextCommand => new Command(() => { LabelName = "Changed Text from Froms"; });

		string _labelName = "Hello from Forms";
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

		Color _formsSelectedColor = Color.Pink;
		public Color FormsSelectedColor
		{
			get { return _formsSelectedColor; }
			set
			{
				if (_formsSelectedColor == value)
					return;
				_formsSelectedColor = value;
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
