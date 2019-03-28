namespace Domain.Models.Response
{
    public class ValidateBonusResponseModel : HeaderModel
    {
        public int id
        {
            get;
            set;
        }

        public string terminal
        {
            get;
            set;
        }

        public string cuc
        {
            get;
            set;
  
        }

        public string nroAprobacion
        {
            get;
            set;
        }

        public string nroBono
        {
            get;
            set;
        }

        public string tipoBono
        {
            get;
            set;
        }

        public string estadoBono
        {
            get;
            set;
        }

        public decimal valorBono
        {
            get;
            set;
        }

        public string idReembolso
        {
            get;
            set;
        }

        public string codigoBono
        {
            get;
            set;
        }
    }
}
