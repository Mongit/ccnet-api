using System;
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

        public IEnumerable<Cuenta> GetAll(Guid id)
        {
            return CuentasDAL.GetAll(id);
        }

        public Guid Save(Cuenta model)
        {
            return CuentasDAL.Save(model);
        }

        public Cuenta GetOne(Guid id)
        {
            return CuentasDAL.GetOne(id);
        }

        public Cuenta Update(Guid id, Cuenta model)
        {
            return CuentasDAL.Update(id, model);
        }

        public void Delete(Guid id)
        {
            CuentasDAL.Delete(id);
        }
    }
}
