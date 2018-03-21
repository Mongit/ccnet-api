using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class Cliente : BaseBO, IFolio
    {
        public int Folio { get; set; }
        public string Contacto { get; set; }
        public string Empresa { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public Guid? DatosFacturacionId { get; set; }
        public DateTime FechaCreado { get; set; }
        public string Domicilio { get; set; }
    }
}
