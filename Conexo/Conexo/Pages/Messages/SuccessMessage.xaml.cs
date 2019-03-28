using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace Conexo.Views.Messages
{
    public partial class SuccessMessage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public SuccessMessage(string message)
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
            labelMensaje.Text = message;

        }
        public SuccessMessage(string title, string message)
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
            labelTitle.Text = title;
            labelMensaje.Text = message;

        }

        async void Aceptar_Tapped(object sender, System.EventArgs e)
        {
            await this.Navigation.PopPopupAsync();
        }
    }
}
