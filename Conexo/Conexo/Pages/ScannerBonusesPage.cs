using System;
using System.ComponentModel;
using Conexo.Views.PopUp;
using Global.Constants.Analytics;
using Domain.Models;
using Domain.Models.Request;
using Domain.Models.Response;
using Infraestructure.Networking;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using Conexo.ViewModels;
using Domain.Services;
using FreshMvvm;
using Common.Dependencies.Firebase;
using Conexo.Classes.Helpers;

namespace Conexo.Views
{


    public class ScannerBonusesPage : ContentPage , INotifyPropertyChanged 
    {


        public delegate void ScannedCodeEventHandler(string code);
        public event ScannedCodeEventHandler OnScannedCode;
        public event PropertyChangedEventHandler PropertyChanged;

        private ILoginService _loginService;
        private NavigationService _navigationService;
        private UserModel _currentUserModel;
        private ZXingScannerView _zxing;
        private ZXingDefaultOverlay _overlay;



        public ZXingScannerView Zxing
        {
            get
            {
                return this._zxing;
            }
            set
            {
                if (this._zxing != value)
                {
                    this._zxing = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Zxing)));
                }
            }
        }




        public ScannerBonusesPage() : base()
        {

            _loginService = FreshIOC.Container.Resolve<ILoginService>();
            _navigationService = FreshIOC.Container.Resolve<NavigationService>();

            _currentUserModel = _loginService.GetUser();
            this.Title = Application.Current.Resources["TitleScannerBonusesPage"].ToString();

            Zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsAnalyzing = true
            };


            Zxing.OnScanResult += (result) => Device.BeginInvokeOnMainThread(async () =>
                {
                    await _navigationService.PopPage();
                    OnScannedCode?.Invoke(result.Text);
                }
            );

            _overlay = new ZXingDefaultOverlay
            {
                ShowFlashButton = Zxing.HasTorch
            };

            _overlay.FlashButtonClicked += (sender, e) =>
            {
                Zxing.IsTorchOn = !Zxing.IsTorchOn;
            };

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            grid.Children.Add(Zxing);
            grid.Children.Add(_overlay);

            Content = grid;
        }



        #region Methods

        protected override void OnAppearing()
        {
            base.OnAppearing();
            FreshIOC.Container.Resolve<IFirebaseAnalyticsDependency>().SetScreenNameAndClass(AnalyticsConstants.SCREEN_SCANNER, this.GetType().Name);
            Zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            Zxing.IsScanning = false;
            base.OnDisappearing();
        }

        #endregion

    }

}
