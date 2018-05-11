using BO;
using System;
using System.Collections.Generic;

namespace api.Properties.Handlers
{
    public interface ICotizacionesHandler
    {
        IEnumerable<Cotizacion> GetAll(Guid id, out int totalPages, int pageNumber, int pageSize);
        Guid Save(Cotizacion model);
        Guid Delete(Guid id);
        IEnumerable<Cotizacion> SearchByTerm(String term);
    }
}
