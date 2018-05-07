using BO.Producto;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL.Productos
{
    public class ProductosDAL : BaseApiDAL<Producto>, IDAL<Producto>
    {
        public ProductosDAL(IConfiguration config, string parentCol = "") : base("PRODUCTOS", config, parentCol)
        {
        }

        public IEnumerable<Producto> GetAll(out int totalPages, int pageNumber = 1, int pageSize = 100)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GET_ALL_SP))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pn = GetParam("@PageNumber", SqlDbType.Int, pageNumber);
                    SqlParameter ps = GetParam("@PageSize", SqlDbType.Int, pageSize);
                    SqlParameter tp = GetParam("@TotalPages", SqlDbType.Int, 0);
                    tp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pn);
                    cmd.Parameters.Add(ps);
                    cmd.Parameters.Add(tp);

                    List<Producto> list = new List<Producto>();
                    Action<SqlDataReader> action = (dr =>
                    {
                        while (dr.Read())
                        {
                            list.Add(Load(dr));
                        }
                    });

                    ExecuteDataReader(cmd, action);

                    totalPages = (int)tp.Value;

                    return list;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Producto> GetAll(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Producto> GetAll(Guid id, out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public override Producto Load(SqlDataReader dr)
        {
            Producto it = new Producto();
            it.Id = GetCastValue<Guid>(dr["Id"]);
            it.Nombre = GetString(dr["Nombre"]);
            it.Color = GetString(dr["Color"]);
            it.Unidad = GetString(dr["Unidad"]);
            it.ProveedorId = GetCastValue<Guid>(dr["ProveedorId"]);
            
            return it;
        }

        public override Guid Save(Producto model)
        {
            SqlCommand cmd = NewCommand(SqlQueries.SAVE_SP, CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, model.Id));
            cmd.Parameters.Add(GetParam("@nombre", SqlDbType.VarChar, model.Nombre));
            cmd.Parameters.Add(GetParam("@color", SqlDbType.VarChar, model.Color));
            cmd.Parameters.Add(GetParam("@unidad", SqlDbType.VarChar, model.Unidad));
            cmd.Parameters.Add(GetParam("@proveedorId", SqlDbType.UniqueIdentifier, model.ProveedorId));

            ExecuteNonQuery(cmd);

            return model.Id;
        }

        public IEnumerable<Producto> SearchByTerm(string term)
        {
            throw new NotImplementedException();
        }

        public Producto Update(Guid id, Producto model)
        {
            SqlCommand cmd = NewCommand(SqlQueries.UPDATE_SP, CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, id));
            cmd.Parameters.Add(GetParam("@nombre", SqlDbType.VarChar, model.Nombre));
            cmd.Parameters.Add(GetParam("@color", SqlDbType.VarChar, model.Color));
            cmd.Parameters.Add(GetParam("@unidad", SqlDbType.VarChar, model.Unidad));
            cmd.Parameters.Add(GetParam("@proveedorId", SqlDbType.UniqueIdentifier, model.ProveedorId));

            ExecuteNonQuery(cmd);

            return model;
        }
    }
}
