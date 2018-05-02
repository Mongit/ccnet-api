using System;

namespace BO.Proveedor
{
    public class Cuenta : BaseBO
    {
        public Guid ProveedorId { get; set; }
        public string Banco { get; set; }
        public string Titular { get; set; }
        public string CLABE { get; set; }
        public string NoCuenta { get; set; }
    }
}
