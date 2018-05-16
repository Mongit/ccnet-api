using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BO.Stock;
using Microsoft.Extensions.Configuration;

namespace DAL.Stocks
{
    public class StocksReportDAL : BaseApiDAL<StockReport>, IDAL<StockReport>
    {
        public StocksReportDAL(IConfiguration config, string parentCol = "") : base("STOCKS", config, parentCol)
        {
        }

        public override StockReport Load(SqlDataReader dr)
        {
            StockReport it = new StockReport();
            it.Id = GetCastValue<Guid>(dr["Id"]);
            it.ProductoId = GetCastValue<Guid>(dr["ProductoId"]);
            it.Cantidad = GetCastValue<decimal>(dr["Cantidad"]);
            it.Precio = GetCastValue<decimal>(dr["Precio"]);
            it.Fecha = GetCastValue<DateTime>(dr["Fecha"]);
            it.ProveedorId = GetCastValue<Guid>(dr["ProveedorId"]);
            it.ReciboId = GetCastValue<Guid>(dr["ReciboId"]);
            it.ProductoNombre = GetString(dr["prod_Nombre"]);
            it.ProveedorEmpresa = GetString(dr["prov_Empresa"]);
            it.ReciboFolio = GetString(dr["r_Folio"]);

            return it;
        }

        public override Guid Save(StockReport model)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<StockReport> GetAll(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StockReport> GetAll(out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StockReport> GetAll(Guid id, out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public StockReport Update(Guid id, StockReport model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StockReport> SearchByTerm(string term)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StockReport> GetReport(out int totalPages, int pageNumber, int pageSize)
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

                    List<StockReport> list = new List<StockReport>();
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
    }
}
