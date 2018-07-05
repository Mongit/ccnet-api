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
            throw new NotImplementedException();
        }

        public IEnumerable<Stock> GetAll(Guid id, out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Stock GetOneReport(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stock> GetReport(out int totalPages, int pageNumber, int pageSize)
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
            SqlCommand cmd = NewCommand(SqlQueries.SAVE_SP, CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, model.Id));
            cmd.Parameters.Add(GetParam("@productoId", SqlDbType.UniqueIdentifier, model.ProductoId));
            cmd.Parameters.Add(GetParam("@cantidad", SqlDbType.Decimal, model.Cantidad));
            cmd.Parameters.Add(GetParam("@precio", SqlDbType.Decimal, model.Precio));
            cmd.Parameters.Add(GetParam("@fecha", SqlDbType.DateTime, model.Fecha));
            cmd.Parameters.Add(GetParam("@proveedorId", SqlDbType.UniqueIdentifier, model.ProveedorId));
            cmd.Parameters.Add(GetParam("@reciboId", SqlDbType.UniqueIdentifier, model.ReciboId));
            
            ExecuteNonQuery(cmd);

            return model.Id;
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
