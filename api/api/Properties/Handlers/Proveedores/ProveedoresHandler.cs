using System;
using System.Collections.Generic;
using BO.Proveedor;
using DAL;
using Microsoft.Extensions.Configuration;

namespace api.Properties.Handlers.Proveedores
{
    public class ProveedoresHandler : IProveedoresHandler
    {
        private IConfiguration Configuration { get; set; }
        IDAL<Proveedor> ProveedoresDAL { get; set; }

        public ProveedoresHandler(IConfiguration config, IDAL<Proveedor> proveedoresDal)
        {
            Configuration = config;
            ProveedoresDAL = proveedoresDal;
        }

        public IEnumerable<Proveedor> GetAll(out int totalPages, int pageNumber, int pageSize)
        {
            return ProveedoresDAL.GetAll(out totalPages, pageNumber, pageSize);
        }

        public Guid Save(Proveedor model)
        {
            return ProveedoresDAL.Save(model);
        }

        public Proveedor GetOne(Guid id)
        {
            return ProveedoresDAL.GetOne(id);
        }
    }
}
