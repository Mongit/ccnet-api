using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace api.Properties.Models
{
    public class PresupuestoItemModel
    {
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
        [JsonProperty(PropertyName = "presupuestoId")]
        public Guid PresupuestoId { get; set; }

        public BO.PresupuestoItem GetBusinessObject(Guid presupuestoId)
        {
            BO.PresupuestoItem item = new BO.PresupuestoItem();
            item.Cantidad = Cantidad;
            item.Descripcion = Descripcion;
            item.Precio = Precio;
            item.PresupuestoId = presupuestoId;

            return item;
        }
    }

    public class PresupuestoModel
    {
        [Required]
        [JsonProperty(PropertyName = "cantidad")]
        public decimal Cantidad { get; set; }
        [Required]
        [JsonProperty(PropertyName = "descripcion")]
        public string Descripcion { get; set; }
        [Required]
        [JsonProperty(PropertyName = "porcentajeGastos")]
        public decimal PorcentajeGastos { get; set; }
        [Required]
        [JsonProperty(PropertyName = "porcentajeGanancia")]
        public decimal PorcentajeGanancia { get; set; }
        [Required]
        [JsonProperty(PropertyName = "porcentajeIva")]
        public decimal PorcentajeIva { get; set; }
        [Required]
        [JsonProperty(PropertyName = "cotizacionId")]
        public Guid CotizacionId { get; set; }
        [JsonProperty(PropertyName = "items")]
        public PresupuestoItemModel[] PresupuestosItem;

        public BO.Presupuesto GetBusinessObject()
        {
            BO.Presupuesto presupuesto = new BO.Presupuesto();
            presupuesto.CotizacionId = CotizacionId;
            presupuesto.Cantidad = Cantidad;
            presupuesto.Descripcion = Descripcion;
            presupuesto.PorcentajeGastos = PorcentajeGastos;
            presupuesto.PorcentajeGanancia = PorcentajeGanancia;
            presupuesto.PorcentajeIVA = PorcentajeIva;

            var parentId = presupuesto.Id;

            foreach (var item in PresupuestosItem)
            {
                var bOject = item.GetBusinessObject(parentId);
                presupuesto.Items.Add(bOject);
            }

            return presupuesto;
        }
    }
}
