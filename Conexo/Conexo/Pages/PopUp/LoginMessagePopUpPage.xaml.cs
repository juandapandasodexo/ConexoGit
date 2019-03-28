using System;
using System.Collections.Generic;
using Conexo.ViewModels.PopUp;
using Xamarin.Forms;

namespace Conexo.Views.PopUp
{
    public partial class LoginMessagePopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {

        public LoginMessagePopUpViewModel ViewModel { get; private set; }

        public LoginMessagePopUpPage(LoginMessagePopUpViewModel viewModel)
        {
            ViewModel = viewModel;
            BindingContext = ViewModel;

            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
        }
    }
}
