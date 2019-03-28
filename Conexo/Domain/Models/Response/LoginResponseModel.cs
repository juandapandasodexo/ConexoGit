using System.Collections.Generic;
using Infraestructure.DTO;

namespace Domain.Models.Response
{
    public class LoginResponseModel : HeaderModel
    {
        
        #region Properties
        public List<CucModel> listPointStore
        {
            get;
            set;
        }


        public string primeraSesion
        {
            get;
            set;
        }

        public string token
        {
            get;
            set;
        }
        #endregion

    }

}
