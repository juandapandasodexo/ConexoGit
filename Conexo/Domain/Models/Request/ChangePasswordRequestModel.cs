namespace Domain.Models.Request
{
    public class ChangePasswordRequestModel
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
