using BO;
using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace api.Properties.Handlers
{
    public class PresupuestosHandler : IPresupuestosHandler
    {
        private IConfiguration Configuration { get; set; }
        IDAL<Presupuesto> PresupuestosDAL { get; set; }
        IDAL<PresupuestoItem> PresupuestoItemDAL { get; set; }

        public PresupuestosHandler(IConfiguration config, IDAL<Presupuesto> PresupuestosDal, IDAL<PresupuestoItem> PresupuestosItemDal)
        {
            Configuration = config;
            PresupuestosDAL = PresupuestosDal;
            PresupuestoItemDAL = PresupuestosItemDal;
        }

        public Guid Save(Presupuesto model)
        {
            Guid saveId = PresupuestosDAL.Save(model);
            return saveId;
        }

        public void SaveChildren(IEnumerable<PresupuestoItem> items)
        {
            PresupuestoItemDAL.SaveChildren(items);
        }
    }
}
