using System;
using System.Collections.Generic;
using Conexo.ViewModels.PopUp;
using Xamarin.Forms;

namespace Conexo.Pages.PopUp
{
    public partial class SellingPointPopUpPage : Rg.Plugins.Popup.Pages.PopupPage
    {

        public SellingPointPopUpViewModel ViewModel
        {
            get;
            set;
        }

        public SellingPointPopUpPage()
        {
            ViewModel = new SellingPointPopUpViewModel();
            BindingContext = ViewModel;

            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
        }
    }
}
