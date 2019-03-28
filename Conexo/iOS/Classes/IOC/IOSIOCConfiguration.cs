using System;
using Common.Dependencies.Error;
using Common.Dependencies.Firebase;
using Common.Dependencies.Mapper;
using Common.Dependencies.Networking;
using Common.Dependencies.SQL;
using Common.Preferences;
using Conexo.iOS.Classes.DependencyClients;
using FreshMvvm;

namespace AppQuo.iOS.Classes.IOC
{
    public class IOSIOCConfiguration
    {
        private IOSIOCConfiguration()
        {

        }

        public static IOSIOCConfiguration Instance { get; } = new IOSIOCConfiguration();

        public void Init()
        {
            //Local Dependencies
            FreshIOC.Container.Register<ISQLiteConnectionDependency,SQLiteConnectionClient>();
            FreshIOC.Container.Register<IMapperDependency,MapperClient>();
            FreshIOC.Container.Register<IErrorReportingDependency,ErrorReportingClient>();
            FreshIOC.Container.Register<INetworkDependency,NetworkClient>();
            FreshIOC.Container.Register<IUserPreferences,UserPreferences>();
            FreshIOC.Container.Register<IFirebaseAnalyticsDependency,FirebaseAnalyticsClient>();
        }
    }
}
