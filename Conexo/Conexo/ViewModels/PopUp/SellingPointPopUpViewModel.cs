using System.Collections.Generic;
using Domain.Models;
using Domain.Services;
using FreshMvvm;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Conexo.ViewModels.PopUp
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class SellingPointPopUpViewModel : FreshBasePageModel
    {

        public const string ON_SELLING_POINT_CHANGED = "OnSellingPointChangedKey";

        private UserModel _userModel;

        private ICUCService _cucService;
        private ILoginService _loginService;

        public List<CucModel> SellingPointCucs { get; set; }
        public CucModel SellingPoint{ get; set; }



        public SellingPointPopUpViewModel()
        {
            _cucService = FreshIOC.Container.Resolve<ICUCService>();
            _loginService = FreshIOC.Container.Resolve<ILoginService>();

            Init();

            this.WhenAny(OnSellingPointChanged, p => p.SellingPoint);
        }

        private void OnSellingPointChanged(string propertyName)
        {
            _cucService.SetCurrentCUC(_userModel.UserName, SellingPoint);
            MessagingCenter.Send<SellingPointPopUpViewModel>(this, ON_SELLING_POINT_CHANGED);
            DestroyThisModalAsync();
        }

        private void Init()
        {
            _userModel = _loginService.GetUser();
            SellingPointCucs = _cucService.GetCurrentCUCList(_userModel.UserName);
        }

        public async void DestroyThisModalAsync()
        {
            await PopupNavigation.Instance.PopAsync();
        }


    }
}
