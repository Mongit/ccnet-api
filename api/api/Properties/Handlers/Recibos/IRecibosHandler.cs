using BO.Recibo;
using System;
using System.Collections.Generic;

namespace api.Properties.Handlers.Recibos
{
    public interface IRecibosHandler
    {
        IEnumerable<Recibo> GetAll(out int totalPages, int pageNumber, int pageSize);
        Guid Save(Recibo model);
        Recibo GetOne(Guid id);
        void Update(Guid id, Recibo model);
        void SaveChildren(IEnumerable<ReciboItem> items);
        void Delete(Guid id);
    }
}
