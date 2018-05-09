using BO.Recibo;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace api.Properties.Models.Recibos
{
    public class ReciboModel
    {
        [JsonProperty(PropertyName = "folio")]
        public int Folio { get; set; }
        [JsonProperty(PropertyName = "clienteId")]
        public Guid ClienteId { get; set; }
        [JsonProperty(PropertyName = "proveedorId")]
        public Guid ProveedorId { get; set; }
        [Required]
        [JsonProperty(PropertyName = "fecha")]
        public DateTime Fecha { get; set; }

        public Recibo GetBusinessObject()
        {
            Recibo recibo = new Recibo();
            recibo.Folio = Folio;
            recibo.ClienteId = ClienteId;
            recibo.ProveedorId = ProveedorId;
            recibo.Fecha = Fecha;

            return recibo;
        }
    }
}
