using BO.Proveedor;
using Newtonsoft.Json;
using System;

namespace api.Properties.Models.Proveedores
{
    public class CuentaModel
    {
        [JsonProperty(PropertyName = "proveedorId")]
        public Guid ProveedorId { get; set; }
        [JsonProperty(PropertyName = "banco")]
        public string Banco { get; set; }
        [JsonProperty(PropertyName = "titular")]
        public string Titular { get; set; }
        [JsonProperty(PropertyName = "clabe")]
        public string CLABE { get; set; }
        [JsonProperty(PropertyName = "noCuenta")]
        public string NoCuenta { get; set; }

        public Cuenta GetBusinessObject()
        {
            Cuenta cuenta = new Cuenta();
            cuenta.ProveedorId = ProveedorId;
            cuenta.Banco = Banco;
            cuenta.Titular = Titular;
            cuenta.CLABE = CLABE;
            cuenta.NoCuenta = NoCuenta;

            return cuenta;
        }
    }
}
