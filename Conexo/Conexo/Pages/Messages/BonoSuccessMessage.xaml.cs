using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace Conexo.Views.Messages
{
    public partial class BonoSuccessMessage : Rg.Plugins.Popup.Pages.PopupPage
    {

        private Action _action;

        public BonoSuccessMessage(string title, string message, Action action)
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
            _action = action;
            labelTitle.Text = title;
            labelMensaje.Text = message;

        }



        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            await this.Navigation.PopPopupAsync();
        }


        async void LeerNuevo_Tapped(object sender, System.EventArgs e)
        {
            await this.Navigation.PopPopupAsync();
            _action?.Invoke();
        }

    }
}
