using System;
using System.Collections.Generic;
using Conexo.ViewModels.PopUp;
using Xamarin.Forms;

namespace Conexo.Views.PopUp
{
    public partial class ForgotPasswordPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {

        public ForgotPasswordPopUpViewModel ViewModel { get; } = new ForgotPasswordPopUpViewModel();
        public ForgotPasswordPopUpPage()
        {
            BindingContext = ViewModel;
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;

        }
    }
}
