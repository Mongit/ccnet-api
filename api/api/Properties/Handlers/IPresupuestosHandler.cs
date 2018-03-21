using BO;
using System;
using System.Collections.Generic;

namespace api.Properties.Handlers
{
    public interface IPresupuestosHandler
    {
        Guid Save(Presupuesto model);
        void SaveChildren(IEnumerable<PresupuestoItem> items);
    }
}
