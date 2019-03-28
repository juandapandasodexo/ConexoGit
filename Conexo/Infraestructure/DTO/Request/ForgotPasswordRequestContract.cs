using System;
namespace Infraestructure.DTO.Request
{
    public class ForgotPasswordRequestContract
    {

        public string typeDocumento
        {
            get;
            set;
        }

        public string nroDocumento
        {
            get;
            set;
        }

        public string usuario
        {
            get;
            set;
        }


    }
}
