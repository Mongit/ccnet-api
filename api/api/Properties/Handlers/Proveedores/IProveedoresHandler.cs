using BO.Proveedor;
using System;
using System.Collections.Generic;

namespace api.Properties.Handlers.Proveedores
{
    public interface IProveedoresHandler
    {
        IEnumerable<Proveedor> GetAll(out int totalPages, int pageNumber, int pageSize);
        Guid Save(Proveedor model);
        Proveedor GetOne(Guid id);
        Proveedor Update(Guid id, Proveedor model);
        void Delete(Guid id);
    }
}
