using System;
using System.Collections.Generic;
using Conexo.ViewModels.PopUp;
using Conexo.Views.PopUp;
using Xamarin.Forms;

namespace Conexo.Views.PopUp
{
    public partial class ConfirmationStatesBonusPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private ConfirmationStatesBonusPopUpViewModel ViewModel;

        public ConfirmationStatesBonusPopUpPage(string title, string description, int icon, string actionLabel, string noActionLabel, Action doAction)
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
            ViewModel = new ConfirmationStatesBonusPopUpViewModel(title, description, icon, actionLabel, noActionLabel, doAction);
            BindingContext = ViewModel;
        }
    }
}
