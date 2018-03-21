using BO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public abstract class BaseApiDAL<TEntity> : BaseDAL
    {
        public ApiQueries SqlQueries { get; set; }

        public BaseApiDAL(string tableName, IConfiguration config, string parentCol = "") : base(config)
        {
            SqlQueries = new ApiQueries(tableName, parentCol);
        }

        public abstract Guid Save(TEntity model);
        public abstract TEntity Load(SqlDataReader dr);
        public virtual void SaveChildren(IEnumerable<TEntity> model)
        {
            throw new NotImplementedException();
        }

        public virtual int Delete(Guid id)
        {
            SqlCommand cmd = NewCommand(SqlQueries.SQL_DELETE_ONE, CommandType.Text);
            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, id));
            return ExecuteNonQuery(cmd);
        }

        public virtual DataTable GetList(int pageNumer, int pageSize, out int totalPages, IDictionary<string, object> parameters)
        {
            try
            {
                using (SqlCommand cmd = NewCommand(SqlQueries.GET_ALL_SP, CommandType.StoredProcedure))
                {
                    SqlParameter pn = GetParam("@PageNumber", SqlDbType.Int, pageNumer);
                    SqlParameter ps = GetParam("@Pagesize", SqlDbType.Int, pageSize);
                    SqlParameter tp = GetParam("@Total Pages", SqlDbType.Int, 0);
                    tp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pn);
                    cmd.Parameters.Add(ps);
                    cmd.Parameters.Add(tp);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet(SqlQueries.TABLE_NAME);
                    adapter.Fill(dataset, SqlQueries.TABLE_NAME);

                    totalPages = (int)tp.Value;
                    return dataset.Tables[0];
                }
            }
            catch
            {
                throw;
            }
        }

        public virtual TEntity GetOne(Guid id)
        {
            SqlCommand cmd = new SqlCommand(SqlQueries.SQL_GET_ONE);
            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, id));

            TEntity it = default(TEntity);

            Action<SqlDataReader> action = (dr =>
            {
                if (dr.Read())
                {
                    it = Load(dr);
                }
            });

            ExecuteDataReader(cmd, action);

            return it;
        }

        public virtual int DeleteChildren(Guid parentId)
        {
            SqlCommand cmd = NewCommand(SqlQueries.SQL_DELETE_CHILDREN, CommandType.Text);
            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, parentId));
            return ExecuteNonQuery(cmd);
        }

        public virtual IEnumerable<TEntity> GetChildren(Guid parentId)
        {
            SqlCommand cmd = new SqlCommand(SqlQueries.SQL_GET_CHILDREN);
            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, parentId));

            List<TEntity> list = new List<TEntity>();

            Action<SqlDataReader> action = (dr =>
            {
                while (dr.Read())
                {
                    list.Add(Load(dr));
                }
            });

            ExecuteDataReader(cmd, action);


            return list;
        }

        public virtual DataTable GetChildrenTable(Guid parentId)
        {
            if (string.IsNullOrEmpty(SqlQueries.SQL_GET_CHILDREN))
                throw new ArgumentNullException("SQL_GET_CHILDREN", "Missing Query");

            SqlCommand cmd = NewCommand(SqlQueries.SQL_GET_CHILDREN, CommandType.Text);
            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, parentId));

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet dataset = new DataSet(SqlQueries.TABLE_NAME);
            adapter.Fill(dataset);
            dataset.Tables[0].TableName = SqlQueries.TABLE_NAME;

            return dataset.Tables[0];
        }

    }
}
