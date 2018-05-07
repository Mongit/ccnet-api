using BO.Producto;
using Newtonsoft.Json;
using System;

namespace api.Properties.Models.Productos
{
    public class ProductoModel
    {
        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }
        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }
        [JsonProperty(PropertyName = "unidad")]
        public string Unidad { get; set; }
        [JsonProperty(PropertyName = "proveedorId")]
        public Guid ProveedorId { get; set; }

        public Producto GetBusinessObject()
        {
            Producto producto = new Producto();
            producto.Nombre = Nombre;
            producto.Color = Color;
            producto.Unidad = Unidad;
            producto.ProveedorId = ProveedorId;

            return producto;
        }
    }
}
