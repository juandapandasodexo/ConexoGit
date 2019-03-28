using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Common.Dependencies.Firebase;
using Common.Dependencies.Resources;
using Conexo.Classes.Helpers;
using Conexo.Classes.Services;
using Conexo.ViewModels.Base;
using Conexo.ViewModels.PopUp;
using Domain.Services;
using FreshMvvm;
using Global.Constants.Analytics;
using Xamarin.Forms;

namespace Conexo.ViewModels
{

    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ChoiceCucViewModel : BaseViewModel
    {
        private MessageService _messageService;
        private PopUpService _popUpService;
        private IResourcesDependency _resourcesDependency; 
        private NavigationService _navigationService;
        private ICUCService _cucService;
        private ILoginService _loginService;
        private IValidBonusService _validBonusService;
        private IFirebaseAnalyticsDependency _firebaseAnalyticsDependency;
        private string _userName;



        public bool IsEnabled { get; set; }
        public bool IsRunning { get; set; }
        public string SellingPointDescription { get; set; }
        public Color ColorButton { get; set; }
        public bool IsEnabledSellingPoint { get; set; }
        public string SellingPointTitle{ get; set; }




        public ICommand StartScanCommand { get {  return new Command(async () => { await OnStartScanEvent(); }); }}
        public ICommand SellingPointCommand{ get { return new Command(async () => { await OnSellingPointAsync(); }); }}
        public ICommand ChangePasswordCommand { get { return new Command(async () => { await OnChangePasswordAsync(); }); } }




        public ChoiceCucViewModel(PopUpService popUpService,NavigationService navigationService,MessageService messageService,IResourcesDependency resourcesDependency,ICUCService cucService,ILoginService loginService,IFirebaseAnalyticsDependency firebaseAnalyticsDependency,IValidBonusService validBonusService)
        {
            _popUpService = popUpService;
            _navigationService = navigationService;
            _messageService = messageService;
            _resourcesDependency = resourcesDependency;
            _cucService = cucService;
            _loginService = loginService;
            _firebaseAnalyticsDependency = firebaseAnalyticsDependency;
            _validBonusService = validBonusService;


            _userName = _loginService.GetUser().UserName;

            // Eventos de cuando cambia las propiuedades
            this.WhenAny(OnIsEnabledSellingPointChanged, p => p.IsEnabledSellingPoint);
            this.WhenAny(OnSellingPointTitleChanged, p => p.SellingPointTitle);

            Init();
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            //Google Analytics
            _firebaseAnalyticsDependency.SetScreenNameAndClass(AnalyticsConstants.SCREEN_CUC_SELECTION, this.GetType().Name);

            //Validamos los codigos. Si no hay codigos redirige al lector
            await RedirectIfHasBonuses();

        }


        void Init()
        {
            MessagingCenter.Subscribe<SellingPointPopUpViewModel>(this, SellingPointPopUpViewModel.ON_SELLING_POINT_CHANGED, (sender) => 
            {
                    ReloadCUCPoint();
            });

            ColorButton = (Color)Application.Current.Resources["GrayTextColor"];
            SellingPointTitle = Application.Current.Resources["SellingPoint"].ToString();
            IsEnabledSellingPoint = false;
            IsEnabled = true;
        }

        void ReloadCUCPoint(){
            var userModel = _loginService.GetUser();
            var currentCUC = _cucService.GetCurrentCUC(userModel.UserName);
            SellingPointTitle = currentCUC.nombrePuntoventa;
            SellingPointDescription = currentCUC.descripcion;
        }

        async Task RedirectIfHasBonuses()
        {
            var userName = _loginService.GetUser().UserName;
            int totalBonos = _validBonusService.GetLocalBonus(userName).Count;
            if (totalBonos > 0)
            {

                await _messageService.ShowLoginMessage(_resourcesDependency.ResolveString("CleanBonusTitle"),
                                                       _resourcesDependency.ResolveString("CleanBonusMessage"),
                                                       async () => { await RedirectToSummary(); });
            }
        }



        void OnIsEnabledSellingPointChanged(string propertyName)
        {
            if (IsEnabledSellingPoint == false)
            {
                ColorButton = (Color)Application.Current.Resources["GrayTextColor"];
            }
            else
            {
                ColorButton = (Color)Application.Current.Resources["HeadingTextColor"]; 
            }
        }

        void OnSellingPointTitleChanged(string propertyName)
        {
            if (SellingPointTitle.Equals(Application.Current.Resources["SellingPoint"].ToString()) == false)
            {
                this.IsEnabled = true;
            }
        }


        async Task RedirectToSummary(){
            //var _summaryBonusesPage = FreshPageModelResolver.ResolvePageModel<SummaryBonusesViewModel>();
            await CoreMethods.PushPageModel<SummaryBonusesViewModel>();
            //await CustomImplementedNav.PushPage(_summaryBonusesPage, null);
            //await CoreMethods.PushPageModel<SummaryBonusesViewModel>();
        }


        async Task OnStartScanEvent()
        {
            if (this.IsEnabled == false || this.IsEnabledSellingPoint == false)
            {
                return;
            }

            this.IsEnabled = false;

            await RedirectToSummary();

            this.IsEnabled = true;
        }

        async Task OnSellingPointAsync()
        {
            if (this.IsEnabled == false)
            {
                return;
            }

            this.IsEnabled = false;
            this.IsRunning = true;


            this.IsEnabledSellingPoint = true;
            await _popUpService.ShowSellingPointSelection();

            this.IsEnabled = true;
            this.IsRunning = false;

        }

        async Task OnChangePasswordAsync()
        {
            if (this.IsEnabled == false)
            {
                return;
            }

            this.IsEnabled = false;
            this.IsRunning = true;

            await _popUpService.ShowChangePasswordAsync(_userName);

            this.IsEnabled = true;
            this.IsRunning = false;

        }

    }
}
