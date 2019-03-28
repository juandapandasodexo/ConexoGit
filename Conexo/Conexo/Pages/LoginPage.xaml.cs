using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Conexo.Pages
{
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
        }

        private void ImageEntry_RightImageClicked(object sender, EventArgs e)
        {
            if (customImageEntry.IsPassword)
            {
                customImageEntry.Source = "eyehide.png";
                customImageEntry.IsPassword = false;
            }
            else
            {
                customImageEntry.Source = "eyeshow.png";
                customImageEntry.IsPassword = true;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            customImageEntry.Source = "eyeshow.png";

        }

    }
}
