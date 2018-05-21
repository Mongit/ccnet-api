﻿using System;

namespace BO.Producto
{
    public class ProductoReport : BaseBO
    {
        public string Nombre { get; set; }
        public string Color { get; set; }
        public decimal Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Proveedor { get; set; }
    }
}
