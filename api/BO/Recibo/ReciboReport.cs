using System;

namespace BO.Recibo
{
    public class ReciboReport : BaseBO
    {
        public int Folio { get; set; }
        public Guid ClienteId { get; set; }
        public Guid ProveedorId { get; set; }
        public DateTime Fecha { get; set; }
        public string ClienteName { get; set; }
        public string ProveedorName { get; set; }
    }
}
