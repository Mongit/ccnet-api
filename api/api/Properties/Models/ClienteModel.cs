using Newtonsoft.Json;
using System;

namespace api.Properties.Models
{
    public class ClienteModel
    {
        [JsonProperty(PropertyName = "folio")]
        public int Folio { get; set; }
        [JsonProperty(PropertyName = "contacto")]
        public string Contacto { get; set; }
        [JsonProperty(PropertyName = "empresa")]
        public string Empresa { get; set; }
        [JsonProperty(PropertyName = "telefono")]
        public string Telefono { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "datosFacturacionId")]
        public Guid? DatosFacturacionId { get; set; }
        [JsonProperty(PropertyName = "fechaCreado")]
        public DateTime FechaCreado { get; set; }
        [JsonProperty(PropertyName = "domicilio")]
        public string Domicilio { get; set; }

        public BO.Cliente GetBusinessObject()
        {
            BO.Cliente cliente = new BO.Cliente();
            cliente.Folio = Folio;
            cliente.Contacto = Contacto;
            cliente.Empresa = Empresa;
            cliente.Telefono = Telefono;
            cliente.Email = Email;
            cliente.DatosFacturacionId = DatosFacturacionId;
            cliente.FechaCreado = FechaCreado;
            cliente.Domicilio = Domicilio;

            return cliente;
        }
    }
}
