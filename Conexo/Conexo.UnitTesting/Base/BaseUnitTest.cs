using System;
using System.Threading.Tasks;
using Conexo.UnitTesting.Classes.Constants;
using Conexo.UnitTesting.Classes.IOC;
using Domain.Models.Request;

namespace Conexo.UnitTesting.Base
{
    public class BaseUnitTest
    {
        public IOCLocator Locator
        {
            get;
            set;
        }

        public BaseUnitTest()
        {
            TestIOCConfiguration.Instance.Init();
            Locator = FreshMvvm.FreshIOC.Container.Resolve<IOCLocator>();
        }


        public async Task Login(){

            var request = new LoginRequestModel()
            {
                UserName = TestConstants.VALID_USERNAME,
                Password = TestConstants.VALID_PASWORD
            };

            await Locator.LogService.Login(request);
        }


    }
}
