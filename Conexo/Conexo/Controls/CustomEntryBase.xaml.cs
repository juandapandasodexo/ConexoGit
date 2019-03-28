using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Conexo.Controls
{
    public partial class CustomEntryBase : ContentView
    {

        public static BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(CustomEntryBase),
            defaultBindingMode: BindingMode.TwoWay);

        public static BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string), 
            typeof(CustomEntryBase),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newval) =>
            {
                var matEntry = (CustomEntryBase)bindable;
                matEntry.customEntry.Placeholder = (string)newval;
            });

        public static BindableProperty IsPasswordProperty = BindableProperty.Create(
            nameof(IsPassword),
            typeof(bool),
            typeof(CustomEntryBase),
            defaultValue: false,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (CustomEntryBase)bindable;
                matEntry.customEntry.IsPassword = (bool)newVal;
            });

        public static BindableProperty KeyboardProperty = BindableProperty.Create(
            nameof(Keyboard),
            typeof(Keyboard),
            typeof(CustomEntryBase),
            defaultValue: Keyboard.Default,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (CustomEntryBase)bindable;
                matEntry.customEntry.Keyboard = (Keyboard)newVal;
            });

        //public static BindableProperty FontSizeProperty = BindableProperty.Create(
            //nameof(FontSize),
            //typeof(double),
            //typeof(CustomEntryBase),
            //defaultBindingMode: BindingMode.TwoWay,
            //propertyChanged: (bindable, oldVal, newVal) =>
            //{
            //    var matEntry = (CustomEntryBase)bindable;
            //    matEntry.customEntry.FontSize = (double)newVal;
            //});

        public static BindableProperty AccentColorProperty = BindableProperty.Create(
            nameof(AccentColor),
            typeof(Color),
            typeof(CustomEntryBase),
            defaultValue: Color.Accent);

        public Color AccentColor
        {
            get => (Color)GetValue(AccentColorProperty);
            set => SetValue(AccentColorProperty, value);
        }

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        //public double FontSize
        //{
        //    get => (double)GetValue(FontSizeProperty);
        //    set => SetValue(FontSizeProperty, value);
        //}


        public CustomEntryBase()
        {
            InitializeComponent();
            customEntry.BindingContext = this;
            customEntry.Focused += (s, a) =>
            {
                BottomBorder.BackgroundColor = (Color)Application.Current.Resources["RedColor"];
                if (string.IsNullOrEmpty(customEntry.Text))
                {
                    customEntry.Placeholder = null;
                }
            };
            customEntry.Unfocused += (s, a) =>
            {
                BottomBorder.BackgroundColor = (Color)Application.Current.Resources["GrayTextColor"];
                if (string.IsNullOrEmpty(customEntry.Text))
                {
                    customEntry.Placeholder = Placeholder;
                }
            };
        }

    }

}