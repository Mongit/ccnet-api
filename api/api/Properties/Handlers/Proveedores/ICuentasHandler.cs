using BO.Proveedor;
using System;
using System.Collections.Generic;

namespace api.Properties.Handlers.Proveedores
{
    public interface ICuentasHandler
    {
        IEnumerable<Cuenta> GetAll(Guid id);
        Guid Save(Cuenta model);
        Cuenta GetOne(Guid id);
        Cuenta Update(Guid id, Cuenta model);
    }
}
