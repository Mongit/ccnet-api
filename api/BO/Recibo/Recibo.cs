using System;
using System.Collections.Generic;

namespace BO.Recibo
{
    public class ReciboItem : BaseBO
    {
        public decimal Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }

        public Guid ReciboId { get; set; }
        public Guid CotizacionId { get; set; }
    }

    public class Recibo : BaseBO
    {
        public Recibo()
        {
            this.Items = new List<ReciboItem>();
        }

        public int Folio { get; set; }
        public Guid ClienteId { get; set; }
        public Guid ProveedorId { get; set; }
        public DateTime Fecha { get; set; }
        public List<ReciboItem> Items { get; set; }
    }
}
