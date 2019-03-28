using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Conexo.Classes.Helpers;
using Domain.Models.Request;
using Domain.Models.Response;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using System.Linq;
using Conexo.Views.PopUp;
using Infraestructure.Networking;
using Conexo.ViewModels.Base;
using Conexo.ViewModels.PopUp;
using Conexo.Views;
using Common.Exceptions;
using Domain.Services;
using Global.Constants.Analytics;
using FreshMvvm;
using Conexo.Classes.Services;
using Common.Dependencies.Firebase;

namespace Conexo.ViewModels
{

    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class SummaryBonusesViewModel : BaseViewModel
    {
        private bool _alreadyAppeared;
        private IValidBonusService _validBonusService;
        private MessageService _messageService;
        private NavigationService _navigationService;
        private IFirebaseAnalyticsDependency _firebaseAnalyticsDependency;
        private ICUCService _cucService;
        private ILoginService _loginService;
        private PopUpService _popupService; 
        private string _userName;

        public bool IsLoading { get; set; }
        public decimal ValorTotal { get; set; }
        public int TotalBonus{ get; set; }
        public string Image{ get; set; }
        public bool IsEnabled{ get; set; }
        public ObservableCollection<ValidateBonusResponseModel> ListValidateBonuses { get; set; }


        public Command ClearBonusCommand { get { return new Command(async () => { await OnClearBonusEvent(); }); } }
        public Command AddBonusCommand { get { return new Command(async () => { await OnAddBonusEvent(); }); } }
        public Command DeleteBonusCommand { get { return new Command<string>(async (string code) => { await OnDeleteBonusEvent(code); }); } }
        public Command FinishBonusCommand { get { return new Command(async () => { await OnFinishBonusEvent(); }); } }


        public SummaryBonusesViewModel(IValidBonusService validBonusService,ILoginService loginService,ICUCService cucService,PopUpService popupService,IFirebaseAnalyticsDependency firebaseAnalyticsDependency,NavigationService navigationService,MessageService messageService)
        {
            _validBonusService = validBonusService;
            _loginService = loginService;
            _cucService = cucService;
            _navigationService = navigationService;
            _popupService = popupService;
            _validBonusService = validBonusService;
            _firebaseAnalyticsDependency = firebaseAnalyticsDependency;
            _messageService = messageService;

            LoadData();
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            _firebaseAnalyticsDependency.SetScreenNameAndClass(AnalyticsConstants.SCREEN_SUMMARY, this.GetType().Name);

            await ValidateBonusAndOpenCamera();
        }

        private async Task ValidateBonusAndOpenCamera()
        {

            var total = GetTotalBonus();
            if (total == 0 && _alreadyAppeared == false)
            {
                await OnAddBonusEvent();
            }
            _alreadyAppeared = true;

        }

        int GetTotalBonus(){
            return _validBonusService.GetLocalBonus(_userName).Count;
        }

        void LoadData()
        {
            _userName = _loginService.GetUser().UserName;

            ListValidateBonuses = new ObservableCollection<ValidateBonusResponseModel>();

            int id = 0;
            ListValidateBonuses.Clear();
            var bonuses = _validBonusService.GetLocalBonus(_userName);
            foreach (var item in bonuses)
            {
                id += 1;
                item.id = id;
                ListValidateBonuses.Add(item);
            }

            ValorTotal = bonuses.Sum(pp => pp.valorBono);
            TotalBonus = bonuses.Count;

            if (TotalBonus > 0)
            {
                Image = "icon_finish";
                IsEnabled = true;
            }
            else
            {
                Image = "icon_finish_b";
                IsEnabled = false;
            }
        }

        async Task OnScannedCode(string code)
        {
            IsLoading = true;
            try
            {
                var validateModel = CreateRequest(code);
                ValidateBonusResponseModel validBonus = await _validBonusService.ValidBonus(validateModel);
                LoadData();
                await _popupService.ShowValidationBonus(validBonus, true, async () =>
                {
                    await OnAddBonusEvent();
                });
            }
            catch(NoAuthException ex){
                await _messageService.HandleException(ex);
            }
            catch (Exception ex)
            {
                var invalidBonus = CreateResponse(ex.Message);
                await _popupService.ShowValidationBonus(invalidBonus,false, async () =>
                {
                    await OnAddBonusEvent();
                });

            }
            finally
            {
                IsLoading = false;
            }
        }

        async Task OnClearBonusEvent()
        {
            try
            {
                IsLoading = true;

                await _messageService.ShowConfirmDeleteAllMessage(async () =>
                {
                    await ClearBonus();
                });
            }
            catch (Exception ex)
            {
                await _messageService.HandleException(ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        async Task OnAddBonusEvent()
        {
            ScannerBonusesPage page = new ScannerBonusesPage();
            page.OnScannedCode += async (code) =>
            {
                await OnScannedCode(code);
            };

            IFreshNavigationService rootNavigation = FreshIOC.Container.Resolve<IFreshNavigationService>(Constants.DefaultNavigationServiceName);
            await rootNavigation.PushPage(page, null);

        }

        async Task OnDeleteBonusEvent(string code)
        {

            try
            {
                IsLoading = true;
                var bono = _validBonusService.GetLocalBonusByCodigoBono(_userName,code);
                await _messageService.ShowConfirmDeleteMessage(bono, async () =>
                {
                    await DeleteBonus(code);
                });
            }
            catch (Exception ex)
            {
                await _messageService.HandleException(ex);
            }
            finally
            {
                IsLoading = false;
            }

        }

        async Task OnFinishBonusEvent()
        {
            if (!IsEnabled)
            {
                return;
            }

            try
            {
                IsLoading = true;
                await _messageService.ShowConfirmFinishAllMessage(async () =>
                {
                    await FinishBono();
                });

            }
            catch (Exception ex)
            {
                await _messageService.HandleException(ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        async Task ClearBonus()
        {
            try
            {
                IsLoading = true;
                var cuc = _cucService.GetCurrentCUC(_userName);
                var validBonus = await _validBonusService.RemoveAllBonus(_userName, cuc);
                await _messageService.ShowSuccessDeleteAllMessage();
                LoadData();
            }
            catch (Exception ex)
            {
                await _messageService.HandleException(ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        async Task DeleteBonus(string code)
        {
            try
            {
                IsLoading = true;
                var validateModel = CreateRequest(code);
                var validBonus = await _validBonusService.RemoveBonus(validateModel);
                await _messageService.ShowSuccessDeleteMessage();
                LoadData();

            }
            catch (Exception ex)
            {
                await _messageService.HandleException(ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        async Task FinishBono()
        {
            try
            {
                IsLoading = true;
                var bono = _validBonusService.GetLocalBonus(_userName).FirstOrDefault();
                _validBonusService.DeleteLocalBonus(_userName);
                await _messageService.ShowSuccessFinishAllMessage(bono);
                LoadData();
                await _navigationService.PopPage();
            }
            catch (Exception ex)
            {
                await _messageService.HandleException(ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        ValidateBonusRequetsModel CreateRequest(string code)
        {

            ValidateBonusRequetsModel validateModel = new ValidateBonusRequetsModel();
            validateModel.cuc = _cucService.GetCurrentCUC(_userName).idPuntoVenta;
            validateModel.codigoBono = code;
            validateModel.idEmisorBono = _userName;
            validateModel.idTerminal = string.Empty;
            validateModel.UserName = _userName;

            return validateModel;
        }

        ValidateBonusResponseModel CreateResponse(string description)
        {
            ValidateBonusResponseModel validateBonus = new ValidateBonusResponseModel();
            validateBonus.Descripcion = description;
            validateBonus.tipoBono = string.Empty;
            validateBonus.valorBono = 0;
            return validateBonus;
        }

    }

}
