using System;
using System.Collections.Generic;

namespace BO
{
    public class PresupuestoItem : BaseBO
    {
        public decimal Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }

        public Guid PresupuestoId { get; set; }
    }

    public class Presupuesto : BaseBO
    {
        public Presupuesto()
        {
            this.Items = new List<PresupuestoItem>();
            this.PorcentajeGanancia = 80;
            this.PorcentajeIVA = 16;
            this.PorcentajeGastos = 5;
        }

        public Guid CotizacionId { get; set; }
        public decimal Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal PorcentajeGastos { get; set; }
        public decimal PorcentajeGanancia { get; set; }
        public decimal PorcentajeIVA { get; set; }
        public List<PresupuestoItem> Items { get; set; }
    }
}
