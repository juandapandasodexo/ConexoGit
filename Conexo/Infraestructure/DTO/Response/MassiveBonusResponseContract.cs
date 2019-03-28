using System;
using System.Collections.Generic;

namespace Infraestructure.DTO.Response
{
    public class MassiveBonusResponseContract
    {
        public string codigo { get; set; }
        public object terminal { get; set; }
        public string cuc { get; set; }
        public int pSalida { get; set; }
        public string descripcion { get; set; }
    }

}
