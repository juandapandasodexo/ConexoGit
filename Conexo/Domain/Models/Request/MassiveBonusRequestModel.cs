using System;
using System.Collections.Generic;

namespace Domain.Models.Request
{
    public class MassiveBonusRequestModel
    {
        public string cuc { get; set; }
        public string idEmisorBono { get; set; }
        public List<string> codigosBonos { get; set; }
    }
}
