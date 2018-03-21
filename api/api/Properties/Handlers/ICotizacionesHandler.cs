using BO;
using System;
using System.Collections.Generic;

namespace api.Properties.Handlers
{
    public interface ICotizacionesHandler
    {
        IEnumerable<Cotizacion> GetAll(Guid id);
        Guid Save(Cotizacion model);
        Guid Delete(Guid id);
    }
}
