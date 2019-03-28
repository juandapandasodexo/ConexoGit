using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using AutoMapper;
using Configuration;
using Foundation;
using UIKit;
using static Conexo.App;
using Common;
using Domain.Services;
using Firebase.Core;
using AppQuo.iOS.Classes.IOC;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Conexo.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

		public class LocalConfig
		{

			public static IMapper Mapper
			{
				get;
				set;
			}
		}

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            AppCenter.Start("7dd2f4f9-b101-4f26-abed-c335e8e46bd6",
                   typeof(Analytics), typeof(Crashes));
            AppCenter.Start("7dd2f4f9-b101-4f26-abed-c335e8e46bd6", typeof(Analytics), typeof(Crashes));
            //Nuget de popups
            Rg.Plugins.Popup.Popup.Init();

            //Asociamos el AutoMapper
            LocalConfig.Mapper = AutomapperConfig.CreateMapper();

            //Inicializamos el IoC (Inyeccion de dependencia)
            IOSIOCConfiguration.Instance.Init();


            global::Xamarin.Forms.Forms.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            LoadApplication(new App());
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, true);

            //UIBarButtonItem.Appearance.SetBackButtonTitlePositionAdjustment(new UIOffset(-100, -60), UIBarMetrics.Default);
            //UIBarButtonItem.Appearance.SetTitlePositionAdjustment(new UIOffset(-100, 1), UIBarMetrics.Default);
            UIBarButtonItem.Appearance.SetBackButtonTitlePositionAdjustment (new UIOffset (-100, 0), UIBarMetrics.Default);

            //Firebase
            Firebase.Core.App.Configure();

            return base.FinishedLaunching(app, options);
        }
    }
}
