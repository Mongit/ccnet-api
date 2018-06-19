using System;

namespace BO.Producto
{
    public class Producto : BaseBO, IFolio
    {
        public int Folio { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
        public string Unidad { get; set; }
        public Guid ProveedorId { get; set; }
    }
}
