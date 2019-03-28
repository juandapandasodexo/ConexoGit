using System;
using System.Collections.Generic;
using Conexo.ViewModels.PopUp;
using Xamarin.Forms;

namespace Conexo.Views.PopUp
{
    public partial class ChangePasswordPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {

        public ChangePasswordPopUpViewModel ViewModel { get; } = new ChangePasswordPopUpViewModel();

        public ChangePasswordPopUpPage()
        {

            BindingContext = ViewModel;
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
        }
    }
}
