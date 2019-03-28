using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Conexo.ViewModels.Base;
using Domain.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Conexo.ViewModels.PopUp
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ConfirmationStatesBonusPopUpViewModel : BaseViewModel
    {
        private Action _doAction;
        private ICUCService _cucService;
        private ILoginService _loginService;
        private string _userName;

        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string SellingPointTitle{ get; set; }
        public string SellingPointDescription{ get; set; }
        public string ActionLabel { get; set; }
        public string NoActionLabel { get; set; }

        public ICommand ConfirmTapCommand { get { return new Command(async () => { await ConfirmTap(); }); } }
        public ICommand CancelTapCommand { get { return new Command(async () => { await DestroyThisModalAsync(); }); } }


        public ConfirmationStatesBonusPopUpViewModel(string title, string description, int icon, string actionLabel, string noActionLabel, Action doAction)
        {
            _doAction = doAction;
            _cucService = FreshMvvm.FreshIOC.Container.Resolve<ICUCService>();
            _loginService = FreshMvvm.FreshIOC.Container.Resolve<ILoginService>();

            _userName = _loginService.GetUser().UserName;

            ConfigureData(title, description, icon, actionLabel, noActionLabel);
        }

        private void ConfigureData(string title,string description, int icon, string actionLabel,string noActionLabel)
        {
            this.Title = title;

            switch(icon)
            {
                  case 1: 
                     {
                    this.Image = "icon_minus";
                    break;
                }
                case 2:
                        {
                    this.Image = "icon_x";
                    break;
                }
                case 3:
                        {
                    this.Image = "icon_send";
                    break;
                }
            }

            this.Description = description;
            this.ActionLabel = actionLabel;
            this.NoActionLabel = noActionLabel;

            var cucModel = _cucService.GetCurrentCUC(_userName);
            this.SellingPointTitle = cucModel.nombrePuntoventa;
            this.SellingPointDescription = cucModel.descripcion;
        }

        public async Task DestroyThisModalAsync()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public async Task ConfirmTap()
        {
            await DestroyThisModalAsync();
            _doAction?.Invoke();
        }

    }
}
