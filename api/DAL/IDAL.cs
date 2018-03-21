using BO;
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

        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Guid id);
        TEntity Update(Guid id, TEntity model);
    }
}
