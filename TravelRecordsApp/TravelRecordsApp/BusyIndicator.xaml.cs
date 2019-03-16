using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BusyIndicator : ContentView
	{
		public BusyIndicator ()
		{
			InitializeComponent ();
		}

#pragma warning disable CS0618 // Type or member is obsolete
        public BindableProperty IsBusyProperty { get; } = BindableProperty.Create<BusyIndicator, bool>(p => p.IsBusy, default(bool), BindingMode.TwoWay, propertyChanged: IsBusyChanged);
#pragma warning restore CS0618 // Type or member is obsolete

        private static void IsBusyChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
            ((BusyIndicator)bindable).Frame.IsVisible = newValue;
            ((BusyIndicator)bindable).ActivityIndicator.IsRunning = newValue;
            ((BusyIndicator)bindable).ActivityIndicator.IsVisible = newValue;
        }

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public void ShowIndicator()
        {
            Frame.IsVisible = true;
            ActivityIndicator.IsVisible = true;
            ActivityIndicator.IsRunning = true;
        }

        public void HideIndicator()
        {
            Frame.IsVisible = false;
            ActivityIndicator.IsVisible = false;
            ActivityIndicator.IsRunning = false;
        }

    }
}