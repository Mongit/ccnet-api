using BO.Proveedor;
using System.Collections.Generic;

namespace api.Properties.Handlers.Proveedores
{
    public interface IProveedoresHandler
    {
        IEnumerable<Proveedor> GetAll(out int totalPages, int pageNumber, int pageSize);
    }
}
