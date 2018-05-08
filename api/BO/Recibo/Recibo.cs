using System;

namespace BO.Recibo
{
    public class Recibo : BaseBO
    {
        public int Folio { get; set; }
        public Guid ClienteId { get; set; }
        public Guid ProveedorId { get; set; }
        public DateTime Fecha { get; set; }
    }
}
