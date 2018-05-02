using BO.Proveedor;
using System.Collections.Generic;

namespace api.Properties.Handlers.Proveedores
{
    public interface ICuentasHandler
    {
        IEnumerable<Cuenta> GetAll();
    }
}
