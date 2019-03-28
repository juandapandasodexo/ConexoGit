using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dependencies.Networking;
using Common.Preferences;
using Global.Constants;
using Infraestructure.DTO.Request;
using Infraestructure.DTO.Response;
using Infraestructure.Networking;
using System.Text.RegularExpressions;

namespace Infraestructure.Repositories.WS
{
    
    public interface IWSLoginRepository
    {
        Task<LoginResponseContract> LoginAsync(LoginRequestContract loginRequestModel);
        Task<ChangePasswordResponseContract> ChangePassword(ChangePasswordRequestContract changePasswordRequestContract);
        Task<ForgotPasswordResponseContract> ForgotPassword(ForgotPasswordRequestContract forgotPasswordRequestContract);
    }


    public class LoginRepository : IWSLoginRepository
    {
        
        private INetworkDependency _networkDependency;
        private IUserPreferences _preferences;


        public LoginRepository(INetworkDependency networkDependency, IUserPreferences userPreferences)
        {
            _networkDependency = networkDependency;
            _preferences = userPreferences;
        }

        public async Task<LoginResponseContract> LoginAsync(LoginRequestContract loginRequestModel)
        {
            string urlRelative = GlobalConfig.API + "/ValidateSesionUser";
            LoginResponseContract loginResponseContract;
            using (var client = new ApiClient(GlobalConfig.BASE_URL, _networkDependency, _preferences))
            {
                loginResponseContract = await client.POSTAsync<LoginResponseContract>(urlRelative, loginRequestModel, false);
            }

            //JDP (Marzo, 2019) Se solicita parametros de salida en minuscula y sin acentos. (Cajeros Exito)
            if (loginResponseContract != null)
            {
                if (loginResponseContract.Descripcion != null)
                {
                    loginResponseContract.Descripcion = RemoveAccents(loginResponseContract.Descripcion);
                }
                foreach (var item in loginResponseContract.listPointStore)
                {
                    if (item.nombrePuntoventa.Length > 0)
                    {
                        item.nombrePuntoventa = RemoveAccents(item.nombrePuntoventa);
                    }
                    if (item.descripcion.Length > 0)
                    {
                        item.descripcion = RemoveAccents(item.descripcion);
                    }
                }
            }

            return loginResponseContract;
        }

        public async Task<ChangePasswordResponseContract> ChangePassword(ChangePasswordRequestContract changePasswordRequestContract)
        {
            string urlRelative = GlobalConfig.API + "/ChangePassword";
            ChangePasswordResponseContract changePasswordResponseContract;
            using (var client = new ApiClient(GlobalConfig.BASE_URL, _networkDependency, _preferences))
            {
                changePasswordResponseContract = await client.POSTAsync<ChangePasswordResponseContract>(urlRelative, changePasswordRequestContract, true);
            }

            return changePasswordResponseContract;
        }

        public async Task<ForgotPasswordResponseContract> ForgotPassword(ForgotPasswordRequestContract forgotPasswordRequestContract)
        {
            string urlRelative = GlobalConfig.API + "/RecoveryPassword";
            ForgotPasswordResponseContract forgotPasswordResponseContract;
            using (var client = new ApiClient(GlobalConfig.BASE_URL, _networkDependency, _preferences))
            {
                forgotPasswordResponseContract = await client.POSTAsync<ForgotPasswordResponseContract>(urlRelative, forgotPasswordRequestContract, false);
            }

            //JDP (Marzo, 2019) Se solicita parametros de salida en minuscula y sin acentos. (Cajeros Exito)
            if (forgotPasswordResponseContract != null)
            {
                if (forgotPasswordResponseContract.Descripcion != null)
                {
                    forgotPasswordResponseContract.Descripcion = RemoveAccents(forgotPasswordResponseContract.Descripcion);
                }
            }

            return forgotPasswordResponseContract;
        }

        //JDP (Marzo, 2019) Se agrega función para colocar todo en minuscula, eliminar acentos y reemplaza ñ.
        private string RemoveAccents(string inputString)
        {
            inputString = inputString.ToLower();
            Regex replace_a_Accents = new Regex("[á|à|ä|â]");
            Regex replace_e_Accents = new Regex("[é|è|ë|ê]");
            Regex replace_i_Accents = new Regex("[í|ì|ï|î]");
            Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]");
            Regex replace_u_Accents = new Regex("[ú|ù|ü|û]");
            Regex replace_enie_Accents = new Regex("[ñ]");
            inputString = replace_a_Accents.Replace(inputString, "a");
            inputString = replace_e_Accents.Replace(inputString, "e");
            inputString = replace_i_Accents.Replace(inputString, "i");
            inputString = replace_o_Accents.Replace(inputString, "o");
            inputString = replace_u_Accents.Replace(inputString, "u");
            inputString = replace_enie_Accents.Replace(inputString, "n");
            return inputString;
        }
    }
}
