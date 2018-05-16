using BO.Producto;
using DAL;
using Microsoft.Extensions.Configuration;
using System;
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

        public void Save(Producto model)
        {
            ProductosDAL.Save(model);
        }

        public Producto GetOne(Guid id)
        {
            return ProductosDAL.GetOne(id);
        }

        public void Update(Guid id, Producto model)
        {
            ProductosDAL.Update(id, model);
        }

        public void Delete(Guid id)
        {
            ProductosDAL.Delete(id);
        }

        public IEnumerable<Producto> SearchByTerm(string term)
        {
            return ProductosDAL.SearchByTerm(term);
        }
    }
}
