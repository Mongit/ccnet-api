using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BO.Producto;
using Microsoft.Extensions.Configuration;

namespace DAL.Productos
{
    public class ProductosReportDAL : BaseApiDAL<ProductoReport>, IDAL<ProductoReport>
    {
        public ProductosReportDAL(IConfiguration config, string parentCol = "") : base("PRODUCTOS", config, parentCol)
        {
        }

        public IEnumerable<ProductoReport> GetAll(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductoReport> GetAll(out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductoReport> GetAll(Guid id, out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductoReport> GetReport(out int totalPages, int pageNumber, int pageSize)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(SqlQueries.REPORT_SP))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pn = GetParam("@PageNumber", SqlDbType.Int, pageNumber);
                    SqlParameter ps = GetParam("@PageSize", SqlDbType.Int, pageSize);
                    SqlParameter tp = GetParam("@TotalPages", SqlDbType.Int, 0);
                    tp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pn);
                    cmd.Parameters.Add(ps);
                    cmd.Parameters.Add(tp);

                    List<ProductoReport> list = new List<ProductoReport>();
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
            };
        }

        public override ProductoReport Load(SqlDataReader dr)
        {
            ProductoReport it = new ProductoReport();
            it.Id = GetCastValue<Guid>(dr["Id"]);
            it.Folio = GetCastValue<int>(dr["Folio"]);
            it.Nombre = GetCastValue<string>(dr["Nombre"]);
            it.Color = GetCastValue<string>(dr["Color"]);
            it.Cantidad = GetCastValue<decimal>(dr["Cantidad"]);
            it.Unidad = GetCastValue<string>(dr["Unidad"]);
            it.Proveedor = GetCastValue<string>(dr["Proveedor"]);

            return it;
        }

        public override Guid Save(ProductoReport model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductoReport> SearchByTerm(string term)
        {
            throw new NotImplementedException();
        }

        public ProductoReport Update(Guid id, ProductoReport model)
        {
            throw new NotImplementedException();
        }
    }
}
