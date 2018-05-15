using System;

namespace BO.Stock
{
    public class Stock : BaseBO
    {
        public Guid ProductoId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }
        public Guid ProveedorId { get; set; }
        public Guid ReciboId { get; set; }
    }
}
