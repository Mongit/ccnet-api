using System;

namespace BO.Stock
{
    public class StockReport : BaseBO
    {
        public Guid ProductoId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }
        public Guid ProveedorId { get; set; }
        public Guid ReciboId { get; set; }
        public string ProductoNombre { get; set; }
        public int ProductoFolio { get; set; }
        public string ProveedorEmpresa { get; set; }
        public string ReciboFolio { get; set; }
    }
}
