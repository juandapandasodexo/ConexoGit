using System;
using Common.Dependencies.Mapper;
using Common.Dependencies.Networking;
using Common.Dependencies.Resources;
using Common.Dependencies.SQL;
using Common.Preferences;
using Conexo.UnitTesting.Classes.DependencyClient;
using Domain.Services;
using Domain.Services.Bonus;
using Domain.Services.CUC;
using Domain.Services.Login;
using FreshMvvm;
using Infraestructure.Repositories.Data;
using Infraestructure.Repositories.WS;

namespace Conexo.UnitTesting.Classes.IOC
{
    public class TestIOCConfiguration
    {

        private TestIOCConfiguration(){
            Init();
        }

        public static TestIOCConfiguration Instance { get; set; } = new TestIOCConfiguration();

        public void Init()
        {

            //Local Dependencies
            FreshIOC.Container.Register<ISQLiteConnectionDependency, SQLiteConnectionClient>();
            FreshIOC.Container.Register<IMapperDependency, MapperClient>();
            FreshIOC.Container.Register<INetworkDependency, NetworkClient>();
            FreshIOC.Container.Register<IUserPreferences, UserPreferences>();
            FreshIOC.Container.Register<IResourcesDependency, ResourceClient>();


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

            //Locator
            FreshIOC.Container.Register<IOCLocator, IOCLocator>().AsSingleton();



        }
    }
}
