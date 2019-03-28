using System.Collections.Generic;

namespace Infraestructure.DTO.Response
{
    public class LoginResponseContract : HeaderContract
    {
        
        #region Properties
        public List<CucContract> listPointStore
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
