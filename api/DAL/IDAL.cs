using BO;
using BO.Proveedor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public interface IDAL<TEntity> where TEntity : BaseBO
    {
        DataTable GetList(int pageNumber, int pageSize, out int TotalPages, IDictionary<string, object> parameters);
        TEntity GetOne(Guid id);
        int Delete(Guid id);
        Guid Save(TEntity model);
        TEntity Load(SqlDataReader dr);

        void SaveChildren(IEnumerable<TEntity> model);
        int DeleteChildren(Guid parentId);
        IEnumerable<TEntity> GetChildren(Guid parentId);
        
        IEnumerable<TEntity> GetAll(Guid id);
        IEnumerable<TEntity> GetAll(out int totalPages, int pageNumber, int pageSize);
        IEnumerable<TEntity> GetAll(Guid id, out int totalPages, int pageNumber, int pageSize);
        TEntity Update(Guid id, TEntity model);
        IEnumerable<TEntity> SearchByTerm(string term);
    }
}
