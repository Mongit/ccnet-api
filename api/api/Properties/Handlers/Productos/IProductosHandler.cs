using BO.Producto;
using System;
using System.Collections.Generic;

namespace api.Properties.Handlers.Productos
{
    public interface IProductosHandler
    {
        IEnumerable<Producto> GetAll(out int totalPages, int pageNumber, int pageSize);
        void Save(Producto model);
        Producto GetOne(Guid id);
        void Update(Guid id, Producto model);
        void Delete(Guid id);
        IEnumerable<Producto> SearchByTerm(String term);
        IEnumerable<ProductoReport> GetReport(out int totalPages, int pageNumber, int pageSize);
        ProductoReport GetOneReport(Guid id);
        IEnumerable<ProductoReport> GetRange(int from, int to);
    }
}
