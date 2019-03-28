using System;
using System.Threading.Tasks;
using Common.Exceptions;
using Conexo.UnitTesting.Base;
using Conexo.UnitTesting.Classes.Constants;
using Conexo.UnitTesting.Classes.IOC;
using Domain.Models.Request;
using FluentValidation;
using Xunit;

namespace Conexo.UnitTesting.Domain
{
    public class LoginTest : BaseUnitTest
    {


        [Fact]
        public async Task Login_NoUsuario_NoContrasena()
        {
            var request = new LoginRequestModel();

            await Assert.ThrowsAsync<ValidationException>(async () => {
                await Locator.LogService.Login(request);
            });
            
        }


        [Fact]
        public async Task Login_Invalido()
        {
            var request = new LoginRequestModel() { 
                UserName = TestConstants.VALID_USERNAME,
                Password = TestConstants.INVALID_PASWORD
            };

            await Assert.ThrowsAsync<WSErrorException>(async () => {
                await Locator.LogService.Login(request);
            });
        }


        [Fact]
        public async Task Login_Valido()
        {
            var request = new LoginRequestModel()
            {
                UserName = TestConstants.VALID_USERNAME,
                Password = TestConstants.VALID_PASWORD
            };

            await Locator.LogService.Login(request);

        }

        [Fact]
        public async Task ChangePassword()
        {
            //Logueamos para que tenga access_token
            var request = new LoginRequestModel()
            {
                UserName = TestConstants.VALID_USERNAME,
                Password = TestConstants.VALID_PASWORD
            };

            await Locator.LogService.Login(request);

            // Cambiamos la clave
            var changePasswordRequestModel = new ChangePasswordRequestModel(){
                usuario = TestConstants.VALID_USERNAME,
                contraseniaActual = TestConstants.VALID_PASWORD,
                contraseniaNueva = TestConstants.NEWVALID_PASWORD
            };

            await Locator.LogService.ChangePassword(changePasswordRequestModel);



            // La devolvemos a lo mismo
            var changePasswordRequestModelComback = new ChangePasswordRequestModel()
            {
                usuario = TestConstants.VALID_USERNAME,
                contraseniaActual = TestConstants.NEWVALID_PASWORD,
                contraseniaNueva = TestConstants.VALID_PASWORD
            };

            await Locator.LogService.ChangePassword(changePasswordRequestModelComback);
        }

    }
}
