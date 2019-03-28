using System;
using Common.Dependencies.Error;
using Common.Dependencies.Firebase;
using Common.Dependencies.Mapper;
using Common.Dependencies.Networking;
using Common.Dependencies.SQL;
using Common.Preferences;
using Conexo.Droid.Classes.DependencyClient;
using Conexo.Droid.DependencyClient;
using Domain.Services;
using Domain.Services.CUC;
using Domain.Services.Login;
using FreshMvvm;
using Infraestructure.Repositories.Data;
using Infraestructure.Repositories.WS;

namespace Conexo.Droid.Classes.IOC
{
    public class AndroidIOCConfiguration
    {
        private AndroidIOCConfiguration()
        {

        }

        public static AndroidIOCConfiguration Instance { get; } = new AndroidIOCConfiguration();

        public void Init()
        {
            //Local Dependencies
            FreshIOC.Container.Register<ISQLiteConnectionDependency, SQLiteConnectionClient>();
            FreshIOC.Container.Register<IMapperDependency, MapperClient>();
            FreshIOC.Container.Register<INetworkDependency, NetworkClient>();
            FreshIOC.Container.Register<IUserPreferences, UserPreferences>();
            FreshIOC.Container.Register<IFirebaseAnalyticsDependency, FirebaseAnalyticsClient>();

        }
    }
}
