using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BO.Recibo;
using Microsoft.Extensions.Configuration;

namespace DAL.Recibos
{
    public class RecibosDAL : BaseApiDAL<Recibo>, IDAL<Recibo>
    {
        public RecibosDAL(IConfiguration config, string parentCol = "") : base("RECIBOS", config, parentCol)
        {
        }

        public IEnumerable<Recibo> GetAll(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recibo> GetAll(out int totalPages, int pageNumber, int pageSize)
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

                    List<Recibo> list = new List<Recibo>();
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

        public IEnumerable<Recibo> GetAll(Guid id, out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recibo> GetReport(out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public override Recibo Load(SqlDataReader dr)
        {
            Recibo it = new Recibo();
            it.Id = GetCastValue<Guid>(dr["Id"]);
            it.Folio = GetCastValue<int>(dr["Folio"]);
            it.ClienteId = GetCastValue<Guid>(dr["ClienteId"]);
            it.ProveedorId = GetCastValue<Guid>(dr["ProveedorId"]);
            it.Fecha = GetCastValue<DateTime>(dr["Fecha"]);
            
            return it;
        }

        public override Guid Save(Recibo model)
        {
            SqlCommand cmd = NewCommand(SqlQueries.SAVE_SP, CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, model.Id));
            cmd.Parameters.Add(GetParam("@clienteId", SqlDbType.UniqueIdentifier, model.ClienteId));
            cmd.Parameters.Add(GetParam("@proveedorId", SqlDbType.UniqueIdentifier, model.ProveedorId));
            cmd.Parameters.Add(GetParam("@fecha", SqlDbType.DateTime, model.Fecha));

            cmd.Parameters.Add(GetParamOut("@folio", SqlDbType.Int));

            ExecuteNonQuery(cmd);

            model.Folio = (int)cmd.Parameters["@folio"].Value;

            return model.Id;
        }

        public IEnumerable<Recibo> SearchByTerm(string term)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Recibos WHERE folio LIKE '%" + term + "%'");

            List<Recibo> list = new List<Recibo>();
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

        public Recibo Update(Guid id, Recibo model)
        {
            SqlCommand cmd = NewCommand(SqlQueries.UPDATE_SP, CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, id));
            cmd.Parameters.Add(GetParam("@folio", SqlDbType.Int, model.Folio));
            cmd.Parameters.Add(GetParam("@clienteId", SqlDbType.UniqueIdentifier, model.ClienteId));
            cmd.Parameters.Add(GetParam("@proveedorId", SqlDbType.UniqueIdentifier, model.ProveedorId));
            cmd.Parameters.Add(GetParam("@fecha", SqlDbType.DateTime, model.Fecha));
            
            ExecuteNonQuery(cmd);

            return model;
        }
    }
}
