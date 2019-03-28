using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace Conexo.Views.Messages
{
    public partial class ErrorMessage : Rg.Plugins.Popup.Pages.PopupPage
    {

        Action _action;
        public ErrorMessage(string errorMessage, Action action = null)
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
            this.labelMensaje.Text = errorMessage;
            this._action = action;
        }

        public ErrorMessage(string title, string errorMessage, Action action = null)
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
            this.labelTitle.Text = title;
            this.labelMensaje.Text = errorMessage;
            this._action = action;
        }

        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            await this.Navigation.PopPopupAsync();
            if (_action != null)
            {
                _action();
            }

        }
    }
}
