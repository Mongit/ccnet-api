using BO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class CotizacionesDAL : BaseApiDAL<Cotizacion>, IDAL<Cotizacion>
    {
        public CotizacionesDAL(IConfiguration config, string v) : base("COTIZACIONES", config, "ClienteId")
        {
        }

        public override DataTable GetList(int pageNumer, int pageSize, out int totalPages, IDictionary<string, object> parameters)
        {
            try
            {
                using (SqlCommand cmd = NewCommand(SqlQueries.GET_ALL_SP, CommandType.StoredProcedure))
                {
                    SqlParameter cl = GetParam("@Client", SqlDbType.UniqueIdentifier, parameters["clienteId"]);
                    SqlParameter pn = GetParam("@PageNumber", SqlDbType.Int, pageNumer);
                    SqlParameter ps = GetParam("@PageSize", SqlDbType.Int, pageSize);
                    SqlParameter tp = GetParam("@TotalPages", SqlDbType.Int, 0);
                    tp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(cl);
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
            catch (Exception e)
            {
                throw e;
            }
        }

        public override Guid Save(Cotizacion e)
        {
            SqlCommand cmd = NewCommand(SqlQueries.SAVE_SP, CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, e.Id));
            cmd.Parameters.Add(GetParam("@clienteId", SqlDbType.UniqueIdentifier, e.ClienteId));
            cmd.Parameters.Add(GetParam("@fecha", SqlDbType.DateTime, e.Fecha));
            ExecuteNonQuery(cmd);

            return e.Id;
        }

        public override Cotizacion Load(SqlDataReader dr)
        {
            Cotizacion it = new Cotizacion();
            it.Id = GetCastValue<Guid>(dr["Id"]);
            it.Folio = GetCastValue<int>(dr["Folio"]);
            it.ClienteId = GetCastValue<Guid>(dr["ClienteId"]);
            it.Fecha = GetCastValue<DateTime>(dr["Fecha"]);

            return it;
        }

        public override void SaveChildren(IEnumerable<Cotizacion> model)
        {
            if (model.Count() == 0) return;
            Cotizacion[] items = model.ToArray();
            Guid parentId = items[0].ClienteId;
            DeleteChildren(parentId);
            DataTable table = GetChildrenTable(parentId);

            foreach (Cotizacion item in items)
            {
                DataRow drItem = GetExistingRowOrNew(table, item.Id);
                drItem["Id"] = item.Id;
                drItem["Folio"] = item.Folio;
                drItem["ClienteId"] = item.ClienteId;
                drItem["Fecha"] = item.Fecha;
            }

            CopyToServer(table);
        }

        public IEnumerable<Cotizacion> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cotizacion> GetAll(Guid id)
        {
            SqlCommand cmd = new SqlCommand("SELECT * from Cotizaciones where ClienteId = '" + id + "'");

            List<Cotizacion> list = new List<Cotizacion>();
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

        public Cotizacion Update(Guid id, Cotizacion model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cotizacion> GetAll(out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cotizacion> GetAll(Guid id, out int totalPages, int pageNumber, int pageSize)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GET_ALL_SP))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter cl = GetParam("@Client", SqlDbType.UniqueIdentifier, id);
                    SqlParameter pn = GetParam("@PageNumber", SqlDbType.Int, pageNumber);
                    SqlParameter ps = GetParam("@PageSize", SqlDbType.Int, pageSize);
                    SqlParameter tp = GetParam("@TotalPages", SqlDbType.Int, 0);
                    tp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(cl);
                    cmd.Parameters.Add(pn);
                    cmd.Parameters.Add(ps);
                    cmd.Parameters.Add(tp);

                    List<Cotizacion> list = new List<Cotizacion>();
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
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
