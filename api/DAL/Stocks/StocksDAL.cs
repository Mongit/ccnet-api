using BO.Stock;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Stocks
{
    public class StocksDAL : BaseApiDAL<Stock>, IDAL<Stock>
    {
        public StocksDAL(IConfiguration config, string parentCol = "") : base("STOCKS", config, parentCol)
        {
        }

        public IEnumerable<Stock> GetAll(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stock> GetAll(out int totalPages, int pageNumber, int pageSize)
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

                    List<Stock> list = new List<Stock>();
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

        public IEnumerable<Stock> GetAll(Guid id, out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public override Stock Load(SqlDataReader dr)
        {
            Stock it = new Stock();
            it.Id = GetCastValue<Guid>(dr["Id"]);
            it.ProductoId = GetCastValue<Guid>(dr["ProductoId"]);
            it.Cantidad = GetCastValue<decimal>(dr["Cantidad"]);
            it.Precio = GetCastValue<decimal>(dr["Precio"]);
            it.Fecha = GetCastValue<DateTime>(dr["Fecha"]);
            it.ProveedorId = GetCastValue<Guid>(dr["ProveedorId"]);
            it.ReciboId = GetCastValue<Guid>(dr["ReciboId"]);

            return it;
        }

        public override Guid Save(Stock model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stock> SearchByTerm(string term)
        {
            throw new NotImplementedException();
        }

        public Stock Update(Guid id, Stock model)
        {
            throw new NotImplementedException();
        }
    }
}
