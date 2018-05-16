using BO.Stock;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace api.Properties.Models.Stocks
{
    public class StockModel
    {
        [Required]
        [JsonProperty(PropertyName = "productoId")]
        public Guid ProductoId { get; set; }
        [Required]
        [JsonProperty(PropertyName = "cantidad")]
        public decimal Cantidad { get; set; }
        [Required]
        [JsonProperty(PropertyName = "precio")]
        public decimal Precio { get; set; }
        [Required]
        [JsonProperty(PropertyName = "fecha")]
        public DateTime Fecha { get; set; }
        [JsonProperty(PropertyName = "proveedorId")]
        public Guid ProveedorId { get; set; }
        [JsonProperty(PropertyName = "reciboId")]
        public Guid ReciboId { get; set; }

        public Stock GetBusinessObject()
        {
            Stock item = new Stock();
            item.ProductoId = ProductoId;
            item.Cantidad = Cantidad;
            item.Precio = Precio;
            item.Fecha = Fecha;
            item.ProveedorId = ProveedorId;
            item.ReciboId = ReciboId;

            return item;
        }

    }
}
