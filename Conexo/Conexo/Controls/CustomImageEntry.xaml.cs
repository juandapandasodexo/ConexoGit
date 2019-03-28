using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Conexo.Controls
{
    public partial class CustomImageEntry : ContentView
    {

        public static BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(CustomImageEntry),
            defaultBindingMode: BindingMode.TwoWay);

        public static BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string), 
            typeof(CustomImageEntry),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newval) =>
            {
                var matEntry = (CustomImageEntry)bindable;
                matEntry.customEntry.Placeholder = (string)newval;
            });

        //public static BindableProperty FontSizeProperty = BindableProperty.Create(
            //nameof(FontSize),
            //typeof(double),
            //typeof(CustomImageEntry),
            //defaultBindingMode: BindingMode.TwoWay,
            //propertyChanged: (bindable, oldVal, newVal) =>
            //{
            //    var matEntry = (CustomImageEntry)bindable;
            //    matEntry.customEntry.FontSize = (double)newVal;
            //});

        public static BindableProperty IsPasswordProperty = BindableProperty.Create(
            nameof(IsPassword),
            typeof(bool),
            typeof(CustomImageEntry),
            defaultValue: false,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (CustomImageEntry)bindable;
                matEntry.customEntry.IsPassword = (bool)newVal;
            });

        public static BindableProperty KeyboardProperty = BindableProperty.Create(
            nameof(Keyboard),
            typeof(Keyboard),
            typeof(CustomImageEntry),
            defaultValue: Keyboard.Default,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (CustomImageEntry)bindable;
                matEntry.customEntry.Keyboard = (Keyboard)newVal;
            });

        public static BindableProperty AccentColorProperty = BindableProperty.Create(
            nameof(AccentColor),
            typeof(Color),
            typeof(CustomImageEntry),
            defaultValue: Color.Accent);

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            nameof(Source),
            typeof(ImageSource),
            typeof(CustomImageEntry),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (CustomImageEntry)bindable;
                matEntry.customImage.Source = (ImageSource)newVal;
            });

        public static readonly BindableProperty ImageAlignmentProperty = BindableProperty.Create(
            nameof(ImageAlignment),
            typeof(eImageAlignment),
            typeof(CustomImageEntry),
            defaultValue: eImageAlignment.None,
            propertyChanged: OnImageAlignmentChanged);
    
        public ImageSource Source
        {
            get => (ImageSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

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

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
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

        //public double FontSize
        //{
        //    get => (double)GetValue(FontSizeProperty);
        //    set => SetValue(FontSizeProperty, value);
        //}

        public eImageAlignment ImageAlignment
        {
            get => (eImageAlignment)GetValue(ImageAlignmentProperty);
            set => SetValue(ImageAlignmentProperty, value);
        }

        public event EventHandler RightImageClicked;

        public virtual void RightImageOn_Clicked(object sender, EventArgs e)
        {
            RightImageClicked?.Invoke(sender, e);
        }


        public CustomImageEntry()
        {
            InitializeComponent();

            customEntry.BindingContext = this;
            customImage.Source = "eyeshow.png";
            customImage.ImageClicked += RightImageOn_Clicked;
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


        private static void OnImageAlignmentChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = bindable as CustomImageEntry;
            switch (control.ImageAlignment)
            {
                case eImageAlignment.None:
                    control.customImage.IsVisible = false;
                    break;
                case eImageAlignment.Right:
                    control.customImage.IsVisible = true;
                    break;
                case eImageAlignment.Password:
                    control.customImage.IsVisible = true;
                    break;
            }
        }


        public enum eImageAlignment
        {
            Left,
            Right,
            Password,
            None
        }

    }
}
