using BO.Producto;
using System.Collections.Generic;

namespace api.Properties.Handlers.Productos
{
    public interface IProductosHandler
    {
        IEnumerable<Producto> GetAll(out int totalPages, int pageNumber, int pageSize);
    }
}
