using BO.Recibo;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace api.Properties.Models.Recibos
{
    public class ReciboItemModel
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        [Required]
        [JsonProperty(PropertyName = "cantidad")]
        public decimal Cantidad { get; set; }
        [Required]
        [JsonProperty(PropertyName = "descripcion")]
        public string Descripcion { get; set; }
        [Required]
        [JsonProperty(PropertyName = "precio")]
        public decimal Precio { get; set; }
        [Required]
        [JsonProperty(PropertyName = "reciboId")]
        public Guid ReciboId { get; set; }
        [JsonProperty(PropertyName = "cotizacionId")]
        public Guid CotizacionId { get; set; }

        public ReciboItem GetBusinessObject(Guid reciboId)
        {
            ReciboItem item = new ReciboItem();
            if (Id != Guid.Empty) item.Id = Id;
            item.Cantidad = Cantidad;
            item.Descripcion = Descripcion;
            item.Precio = Precio;
            item.ReciboId = reciboId;
            item.CotizacionId = CotizacionId;

            return item;
        }
    }

    public class ReciboModel
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        [JsonProperty(PropertyName = "folio")]
        public int Folio { get; set; }
        [JsonProperty(PropertyName = "clienteId")]
        public Guid ClienteId { get; set; }
        [JsonProperty(PropertyName = "proveedorId")]
        public Guid ProveedorId { get; set; }
        [Required]
        [JsonProperty(PropertyName = "fecha")]
        public DateTime Fecha { get; set; }
        [JsonProperty(PropertyName = "items")]
        public ReciboItemModel[] ReciboItems;

        public Recibo GetBusinessObject()
        {
            Recibo recibo = new Recibo();
            recibo.Id = Id == Guid.Empty ? recibo.Id : Id;
            recibo.Folio = Folio;
            recibo.ClienteId = ClienteId;
            recibo.ProveedorId = ProveedorId;
            recibo.Fecha = Fecha;

            var parentId = recibo.Id;

            foreach (var item in ReciboItems)
            {
                var bOject = item.GetBusinessObject(parentId);
                recibo.Items.Add(bOject);
            }

            return recibo;
        }
    }
}
