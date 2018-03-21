using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace api.Properties.Models
{
    public class CotizacionModel
    {
        [Required]
        [JsonProperty(PropertyName = "folio")]
        public int Folio { get; set; }
        [Required]
        [JsonProperty(PropertyName = "clienteId")]
        public Guid ClienteId { get; set; }
        [Required]
        [JsonProperty(PropertyName = "fecha")]
        public DateTime Fecha { get; set; }

        public BO.Cotizacion GetBusinessObject()
        {
            BO.Cotizacion cotizacion = new BO.Cotizacion();
            cotizacion.Folio = Folio;
            cotizacion.ClienteId = ClienteId;
            cotizacion.Fecha = Fecha;

            return cotizacion;
        }
    }
}
