using System;
using Conexo.Classes.Helpers;
using Domain.Services;
using FreshMvvm;
using System.Linq;
using System.Reflection;
using Infraestructure.Repositories.Data;
using Infraestructure.Repositories.WS;
using Domain.Services.Login;
using Common.Dependencies.Resources;
using Conexo.Classes.DependencyClient;
using Conexo.Classes.Services;
using Domain.Services.CUC;
using Domain.Services.Bonus;

namespace Conexo.Classes.IOC
{
    public class FormsIOCConfiguration
    {
        private FormsIOCConfiguration()
        {

        }

        public static FormsIOCConfiguration Instance { get; } = new FormsIOCConfiguration();

        public void Init()
        {


            //Loca Services
            FreshIOC.Container.Register<NavigationService, NavigationService>();
            FreshIOC.Container.Register<PopUpService, PopUpService>();
            FreshIOC.Container.Register<MessageService, MessageService>();
            FreshIOC.Container.Register<IResourcesDependency, ResourcesDependency>();



            //Domain Services
            FreshIOC.Container.Register<ICUCService, CUCService>();
            FreshIOC.Container.Register<ILoginService, LoginService>();
            FreshIOC.Container.Register<IValidBonusService, ValidBonusService>();


            //Repository DATA
            FreshIOC.Container.Register<IBonusDataRepository, BonusDataRepository>();
            FreshIOC.Container.Register<ITokenDataRepository, TokenDataRepository>();
            FreshIOC.Container.Register<IUserDataRepository, UserDataRepository>();

            //Repository WS
            FreshIOC.Container.Register<IWSLoginRepository, LoginRepository>();
            FreshIOC.Container.Register<IWSValidBonoRepository, ValidBonoRepository>();


        }



    }
}
