using BO.Recibo;
using System.Collections.Generic;

namespace api.Properties.Handlers.Recibos
{
    public interface IRecibosHandler
    {
        IEnumerable<Recibo> GetAll(out int totalPages, int pageNumber, int pageSize);
    }
}
