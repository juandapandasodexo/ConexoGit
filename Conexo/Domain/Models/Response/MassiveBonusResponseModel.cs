using System;
namespace Domain.Models.Response
{
    public class MassiveBonusResponseModel
    {
        public string codigo { get; set; }
        public object terminal { get; set; }
        public string cuc { get; set; }
        public int pSalida { get; set; }
        public string descripcion { get; set; }
    }
}
