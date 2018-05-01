using BO.Proveedor;
using Newtonsoft.Json;

namespace api.Properties.Models
{
    public class ProveedorModel
    {
        [JsonProperty(PropertyName = "empresa")]
        public string Empresa { get; set; }
        [JsonProperty(PropertyName = "contacto")]
        public string Contacto { get; set; }
        [JsonProperty(PropertyName = "domicilio")]
        public string Domicilio { get; set; }
        [JsonProperty(PropertyName = "telefono")]
        public string Telefono { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "horarioAtencion")]
        public string HorarioAtencion { get; set; }

        public Proveedor GetBusinessObject()
        {
            Proveedor proveedor = new Proveedor();
            proveedor.Empresa = Empresa;
            proveedor.Contacto = Contacto;
            proveedor.Domicilio = Domicilio;
            proveedor.Telefono = Telefono;
            proveedor.Email = Email;
            proveedor.HorarioAtencion = HorarioAtencion;

            return proveedor;
        }
    }
}
