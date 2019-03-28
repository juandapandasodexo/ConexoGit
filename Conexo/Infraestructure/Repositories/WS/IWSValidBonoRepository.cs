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
    public interface IWSValidBonoRepository
    {
        Task<ValidateBonusResponseContract> ValidBonus(ValidateBonusRequetsContract validateBonusRequetsContract);
        Task<ValidateBonusResponseContract> RemoveBonus(ValidateBonusRequetsContract validateBonusRequetsContract);
        Task<MassiveBonusResponseContract> MassiveRemoveBonus(MassiveBonusRequestContract requestContract);
    }

    public class ValidBonoRepository : IWSValidBonoRepository
    {
        private INetworkDependency _networkDependency;
        private IUserPreferences _preferences;

        public ValidBonoRepository(INetworkDependency networkDependency, IUserPreferences userPreferences)
        {
            _networkDependency = networkDependency;
            _preferences = userPreferences;
        }

        public async Task<ValidateBonusResponseContract> ValidBonus(ValidateBonusRequetsContract validateBonusRequetsContract)
        {
            string urlRelative = GlobalConfig.API + "/ValidateBonus";
            ValidateBonusResponseContract validateBonusResponseContract;
            using (var client = new ApiClient(GlobalConfig.BASE_URL, _networkDependency, _preferences))
            {
                validateBonusResponseContract = await client.POSTAsync<ValidateBonusResponseContract>(urlRelative, validateBonusRequetsContract, true);
            }

            //JDP (Marzo, 2019) Se solicita parametros de salida en minuscula y sin acentos. (Cajeros Exito)
            if (validateBonusResponseContract != null)
            {
                if (validateBonusResponseContract.tipoBono != null)
                {
                    validateBonusResponseContract.tipoBono = RemoveAccents(validateBonusResponseContract.tipoBono);
                }
                if (validateBonusResponseContract.Descripcion != null)
                {
                    validateBonusResponseContract.Descripcion = RemoveAccents(validateBonusResponseContract.Descripcion);
                }
            }

            return validateBonusResponseContract;
        }


        public async Task<ValidateBonusResponseContract> RemoveBonus(ValidateBonusRequetsContract validateBonusRequetsContract)
        {
            string urlRelative = GlobalConfig.API + "/RemoveBonus";
            ValidateBonusResponseContract validateBonusResponseContract;
            using (var client = new ApiClient(GlobalConfig.BASE_URL, _networkDependency, _preferences))
            {
                validateBonusResponseContract = await client.POSTAsync<ValidateBonusResponseContract>(urlRelative, validateBonusRequetsContract, true);
            }

            //JDP (Marzo, 2019) Se solicita parametros de salida en minuscula y sin acentos. (Cajeros Exito)
            if (validateBonusResponseContract != null)
            {
                if (validateBonusResponseContract.tipoBono != null)
                {
                    validateBonusResponseContract.tipoBono = RemoveAccents(validateBonusResponseContract.tipoBono);
                }
                if (validateBonusResponseContract.Descripcion != null)
                {
                    validateBonusResponseContract.Descripcion = RemoveAccents(validateBonusResponseContract.Descripcion);
                }
            }

            return validateBonusResponseContract;
        }

        public async Task<MassiveBonusResponseContract> MassiveRemoveBonus(MassiveBonusRequestContract requestContract)
        {
            string urlRelative = GlobalConfig.API + "/RemoveMassiveBonus";
            MassiveBonusResponseContract responseContract;
            using (var client = new ApiClient(GlobalConfig.BASE_URL, _networkDependency, _preferences))
            {
                responseContract = await client.POSTAsync<MassiveBonusResponseContract>(urlRelative, requestContract, true);
            }

            return responseContract;
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
