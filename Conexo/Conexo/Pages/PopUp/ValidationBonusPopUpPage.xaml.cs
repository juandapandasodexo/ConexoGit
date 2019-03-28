using System;
using System.Collections.Generic;
using Conexo.ViewModels.PopUp;
using Xamarin.Forms;

namespace Conexo.Pages.PopUp
{
    public partial class ValidationBonusPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ValidationBonusPopUpViewModel ViewModel
        {
            get;
            private set;
        }
        public ValidationBonusPopUpPage(ValidationBonusPopUpViewModel viewModel)
        {
            ViewModel = viewModel;
            BindingContext = ViewModel;
            InitializeComponent();
        }
    }
}
