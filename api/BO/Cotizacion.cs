using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class Cotizacion : BaseBO, IFolio
    {
        public int Folio { get; set; }
        public Guid ClienteId { get; set; }
        public DateTime Fecha { get; set; }
    }
}
