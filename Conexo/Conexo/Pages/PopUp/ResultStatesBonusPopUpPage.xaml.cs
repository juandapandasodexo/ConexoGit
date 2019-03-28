using System;
using System.Collections.Generic;
using Conexo.ViewModels.PopUp;
using Xamarin.Forms;

namespace Conexo.Pages.PopUp
{
    public partial class ResultStatesBonusPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private ResultStatesBonusPopUpViewModel ViewModel;

        public ResultStatesBonusPopUpPage(string title, string description, string noActionLabel)
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
            ViewModel = new ResultStatesBonusPopUpViewModel(title, description, noActionLabel);
            BindingContext = ViewModel;
        }
    }
}
