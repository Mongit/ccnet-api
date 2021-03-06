﻿using BO;
using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace api.Properties.Handlers
{
    public class CotizacionesHandler : ICotizacionesHandler
    {
        private IConfiguration Configuration { get; set; }
        IDAL<Cotizacion> CotizacionesDAL { get; set; }
        IDAL<Presupuesto> PresupuestosDAL { get; set; }
        IDAL<PresupuestoItem> PresupuestoItemsDAL { get; set; }

        public CotizacionesHandler(IConfiguration config, IDAL<Cotizacion> CotizacionesDal, IDAL<Presupuesto> PresupuestosDal, IDAL<PresupuestoItem> PresupuestoItemsDal)
        {
            Configuration = config;
            CotizacionesDAL = CotizacionesDal;
            PresupuestosDAL = PresupuestosDal;
            PresupuestoItemsDAL = PresupuestoItemsDal;
        }

        public IEnumerable<Cotizacion> GetAll(Guid id, out int totalPages, int pageNumber, int pageSize)
        {
            return CotizacionesDAL.GetAll(id, out totalPages, pageNumber, pageSize);
        }

        public Guid Save(Cotizacion model)
        {
            Guid savedId = CotizacionesDAL.Save(model);
            return savedId;
        }

        public Guid Delete(Guid cotizacionId)
        {
            IEnumerable<Presupuesto> presupuestos = PresupuestosDAL.GetChildren(cotizacionId);
            foreach (var presupuesto in presupuestos)
            {
                PresupuestoItemsDAL.DeleteChildren(presupuesto.Id);
            }
            PresupuestosDAL.DeleteChildren(cotizacionId);
            CotizacionesDAL.Delete(cotizacionId);
            return cotizacionId;
        }

        public IEnumerable<Cotizacion> SearchByTerm(string term)
        {
            return CotizacionesDAL.SearchByTerm(term);
        }

        public Cotizacion GetOne(Guid id)
        {
            return CotizacionesDAL.GetOne(id);
        }
    }
}
