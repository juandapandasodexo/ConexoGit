using System;
using System.Collections.Generic;

namespace Infraestructure.DTO.Request
{
    public class MassiveBonusRequestContract
    {
        public string cuc { get; set; }
        public string idEmisorBono { get; set; }
        public List<string> codigosBonos { get; set; }
    }
}
