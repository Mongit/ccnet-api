using BO.Producto;
using DAL;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace api.Properties.Handlers.Productos
{
    public class ProductosHandler : IProductosHandler
    {
        private IConfiguration Configuration { get; set; }
        IDAL<Producto> ProductosDAL { get; set; }
        
        public ProductosHandler(IConfiguration config, IDAL<Producto> productosDAL)
        {
            Configuration = config;
            ProductosDAL = productosDAL;
        }

        public IEnumerable<Producto> GetAll(out int totalPages, int pageNumber, int pageSize)
        {
            return ProductosDAL.GetAll(out totalPages, pageNumber, pageSize);
        }
    }
}
