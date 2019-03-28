using System;
using Common.Dependencies.Mapper;
using Common.Dependencies.Networking;
using Common.Dependencies.Resources;
using Common.Dependencies.SQL;
using Common.Preferences;
using Domain.Services;
using Infraestructure.Repositories.Data;
using Infraestructure.Repositories.WS;

namespace Conexo.UnitTesting.Classes.IOC
{
    public class IOCLocator
    {

        public ISQLiteConnectionDependency SQLiteConnectionDependency { get; set; }
        public IMapperDependency MapperDependency { get; set; }
        public INetworkDependency NetworkDependency { get; set; }
        public IUserPreferences UserPreferences { get; set; }



        public ICUCService CUCService { get; set; }
        public ILoginService LogService { get; set; }
        public IValidBonusService ValidBonusService { get; set; }


        public IBonusDataRepository BonusDataRepository { get; set; }
        public ITokenDataRepository TokenDataRepository { get; set; }
        public IUserDataRepository UserDataRepository { get; set; }

        public IWSLoginRepository WSLoginRepository { get; set; }
        public IWSValidBonoRepository WSValidBonoRepository { get; set; }


        public IOCLocator(ISQLiteConnectionDependency sqliiteConnectionDependency, IMapperDependency mapperDependency,
                         INetworkDependency networkDependency, IUserPreferences userPreferences,
                         ICUCService cucService, ILoginService logService, IValidBonusService validBonusService,
                         IBonusDataRepository bonusDataRepository, ITokenDataRepository tokenDataRepository,
                         IUserDataRepository userDataRepository, IWSLoginRepository wsLoginRepository,
                         IWSValidBonoRepository wsValidBonoRepository)
        {

            SQLiteConnectionDependency = sqliiteConnectionDependency;
            MapperDependency = mapperDependency;
            NetworkDependency = networkDependency;
            UserPreferences = userPreferences;


            CUCService = cucService;
            LogService = logService;
            ValidBonusService = validBonusService;


            BonusDataRepository = bonusDataRepository;
            TokenDataRepository = tokenDataRepository;
            UserDataRepository = userDataRepository;


            WSLoginRepository = wsLoginRepository;
            WSValidBonoRepository = wsValidBonoRepository;

        }

    }
}