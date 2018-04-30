using BO;
using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Presupuesto> GetAll(Guid id)
        {
            IEnumerable<Presupuesto> presupuestos =  PresupuestosDAL.GetChildren(id);
            foreach(Presupuesto presupuesto in presupuestos)
            {
                IEnumerable<PresupuestoItem> items =  PresupuestoItemDAL.GetChildren(presupuesto.Id);
                presupuesto.Items = items.ToList();
            }
            return presupuestos;
        }

        public Presupuesto Update(Guid id, Presupuesto model)
        {
            this.SaveChildren(model.Items);
            BO.Presupuesto modelUpdated = PresupuestosDAL.Update(id, model);
            return modelUpdated;
        }

        public void Delete(Guid id)
        {
            PresupuestoItemDAL.DeleteChildren(id);
            PresupuestosDAL.Delete(id);
        }
    }
}
