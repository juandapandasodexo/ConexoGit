
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AutoMapper;
using Conexo.Droid.Classes.IOC;
using Configuration;

namespace Conexo.Droid
{
    [Application]
    public class LocalApp : Application
    {

        private static LocalApp _instance;




        public class LocalConfig
        {

            public static IMapper Mapper
            {
                get;
                set;
            }
        }


        public LocalApp(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
            _instance = this;
        }


        public static LocalApp GetInstance()
        {
            return _instance;
        }

        public override void OnCreate()
        {
            base.OnCreate();

            //Asociamos el AutoMapper
            LocalConfig.Mapper = AutomapperConfig.CreateMapper();

        }

        static class ForceI18NInclusion
        {
            static bool falseflag = false;
            static ForceI18NInclusion()
            {
                if (falseflag)
                {
                    var ignore = new I18N.West.CP1252();
                }
            }
        }
    }
}
