using System;
using System.ComponentModel;
using System.Windows.Input;
using Conexo.Classes;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Conexo.ViewModels.PopUp
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class LoginMessagePopUpViewModel
    {
        private Action _action;


        public string Title { get; set; }
        public string Description { get; set; }
        public string TextAction { get; set; }


        public ICommand CancelTapCommand { get { return new Command(CancelTap); } }

        public LoginMessagePopUpViewModel(string title, string description, string textAction, Action action = null)
        {
            this.Title = title;
            this.Description = description;
            this.TextAction = textAction;
            _action = action;
        }

        public LoginMessagePopUpViewModel(string title, string description, Action action = null)
        {
            this.Title = title;
            this.Description = description;
            this.TextAction = Application.Current.Resources["ButtonLoginMessagePage"].ToString();
            _action = action;
        }

        public async void DestroyThisModalAsync()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public void CancelTap()
        {
            DestroyThisModalAsync();
            _action?.Invoke();
        }
    }
}
