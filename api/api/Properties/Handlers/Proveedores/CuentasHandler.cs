using System.Collections.Generic;
using BO.Proveedor;
using DAL;
using Microsoft.Extensions.Configuration;
namespace api.Properties.Handlers.Proveedores
{
    public class CuentasHandler : ICuentasHandler
    {
        private IConfiguration Configuration { get; set; }
        IDAL<Cuenta> CuentasDAL { get; set; }

        public CuentasHandler(IConfiguration config, IDAL<Cuenta> cuentasDal)
        {
            Configuration = config;
            CuentasDAL = cuentasDal;
        }

        public IEnumerable<Cuenta> GetAll()
        {
            return CuentasDAL.GetAll();
        }
    }
}
