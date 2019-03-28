using System;
using System.Data.Common;
using System.Threading.Tasks;
using Common.Exceptions;
using Conexo.UnitTesting.Base;
using Conexo.UnitTesting.Classes.Constants;
using Domain.Models.Request;
using Xunit;

namespace Conexo.UnitTesting.Domain
{
    public class BonusTest : BaseUnitTest
    {

        [Fact]
        public async Task Bonus_ValidBonus()
        {

            // Obtenemos el access_token
            await base.Login();



            //Preparamos la peticion
            var request = new ValidateBonusRequetsModel()
            {
                cuc = TestConstants.VALID_CUC,
                idTerminal = "",
                idEmisorBono = TestConstants.VALID_USERNAME,
                codigoBono = TestConstants.VALID_CODE
            };

            //Limpiamos el bono
            await ClearBono(request);


            //Ejecutamos
            var response = await Locator.ValidBonusService.ValidBonus(request);


            //Validamos
            Assert.Same(response.codigoBono, TestConstants.VALID_CODE);


            //Limpiamos el bono
            await ClearBono(request);

        }

        private async Task ClearBono(ValidateBonusRequetsModel request)
        {
            try
            {
                await Locator.ValidBonusService.RemoveBonus(request);
            }
            catch (Exception ex) { }
        }

        [Fact]
        public async Task Bonus_InvalidBonus()
        {

            // Obtenemos el access_token
            await base.Login();


            //Preparamos la peticion
            ValidateBonusRequetsModel request = BuildValidBonusRequest();


            //Ejecutamos
            await Assert.ThrowsAsync<WSErrorException>(async () =>
            {
                await Locator.ValidBonusService.ValidBonus(request);
            });


        }

        private static ValidateBonusRequetsModel BuildValidBonusRequest()
        {
            return new ValidateBonusRequetsModel()
            {
                cuc = TestConstants.VALID_CUC,
                idTerminal = "",
                idEmisorBono = TestConstants.VALID_USERNAME,
                codigoBono = TestConstants.INVALID_CODE
            };
        }



    }
}
