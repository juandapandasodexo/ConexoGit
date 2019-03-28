namespace Infraestructure.DTO.Request
{
    public class ChangePasswordRequestContract
    {
        public string usuario
        {
            get;
            set;      
        }

        public string contraseniaActual
        {
            get;
            set;
        }

        public string contraseniaNueva
        {
            get;
            set;
        }
    }
}
