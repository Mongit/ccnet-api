using BO;
using System;
using System.Collections.Generic;

namespace api.Properties.Handlers
{
    public interface IPresupuestosHandler
    {
        Guid Save(Presupuesto model);
        void SaveChildren(IEnumerable<PresupuestoItem> items);
        IEnumerable<Presupuesto> GetAll(Guid id);
        Presupuesto Update(Guid id, Presupuesto model);
        void Delete(Guid id);
    }
}
