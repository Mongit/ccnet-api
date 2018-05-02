using BO.Proveedor;
using System;
using System.Collections.Generic;

namespace api.Properties.Handlers.Proveedores
{
    public interface ICuentasHandler
    {
        IEnumerable<Cuenta> GetAll();
        Guid Save(Cuenta model);
    }
}
