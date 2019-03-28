using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Conexo.Controls
{
    public partial class BusyIndicator : ContentView
    {
        public BusyIndicator()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty IsBusyProperty =
            BindableProperty.Create<BusyIndicator, bool>(p => p.IsBusy, default(bool), BindingMode.TwoWay, propertyChanged: IsBusyChanged);

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

    }
}
