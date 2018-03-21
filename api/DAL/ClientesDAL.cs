using BO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class ClientesDAL : BaseApiDAL<Cliente>, IDAL<Cliente>
    {
        public ClientesDAL(IConfiguration config, string parentCol = "") : base("CLIENTES", config, parentCol)
        {
        }

        public IEnumerable<Cliente> GetAll()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Clientes");

            List<Cliente> list = new List<Cliente>();
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

        public override Cliente Load(SqlDataReader dr)
        {
            Cliente it = new Cliente();
            it.Id = GetCastValue<Guid>(dr["Id"]);
            it.Folio = GetCastValue<int>(dr["Folio"]);
            it.Contacto = GetString(dr["Contacto"]);
            it.Empresa = GetString(dr["Empresa"]);
            it.Telefono = GetString(dr["Telefono"]);
            it.Email = GetString(dr["Email"]);
            it.Domicilio = GetString(dr["Domicilio"]);
            it.FechaCreado = GetCastValue<DateTime>(dr["FechaCreado"]);

            it.DatosFacturacionId = GetNulableGuid(dr["DatosFacturacionId"]);

            return it;
        }

        public override Guid Save(Cliente model)
        {
            SqlCommand cmd = NewCommand(SqlQueries.SAVE_SP, CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, model.Id));
            cmd.Parameters.Add(GetParam("@contacto", SqlDbType.VarChar, model.Contacto));
            cmd.Parameters.Add(GetParam("@empresa", SqlDbType.VarChar, model.Empresa));
            cmd.Parameters.Add(GetParam("@telefono", SqlDbType.VarChar, model.Telefono));
            cmd.Parameters.Add(GetParam("@email", SqlDbType.VarChar, model.Email));
            cmd.Parameters.Add(GetParam("@domicilio", SqlDbType.VarChar, model.Domicilio));
            cmd.Parameters.Add(GetParam("@fechaCreado", SqlDbType.DateTime, model.FechaCreado));
            cmd.Parameters.Add(GetParam("@datosFacturacionId", SqlDbType.UniqueIdentifier, (object)model.DatosFacturacionId ?? DBNull.Value));

            cmd.Parameters.Add(GetParamOut("@folio", SqlDbType.Int));

            int count = ExecuteNonQuery(cmd);

            model.Folio = (int)cmd.Parameters["@folio"].Value;

            return model.Id;
        }

        public Cliente Update(Guid id, Cliente model)
        {
            SqlCommand cmd = NewCommand(SqlQueries.UPDATE_SP, CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, id));
            cmd.Parameters.Add(GetParam("@contacto", SqlDbType.VarChar, model.Contacto));
            cmd.Parameters.Add(GetParam("@empresa", SqlDbType.VarChar, model.Empresa));
            cmd.Parameters.Add(GetParam("@domicilio", SqlDbType.VarChar, model.Domicilio));
            cmd.Parameters.Add(GetParam("@telefono", SqlDbType.VarChar, model.Telefono));
            cmd.Parameters.Add(GetParam("@email", SqlDbType.VarChar, model.Email));
            cmd.Parameters.Add(GetParam("@datosFacturacionId", SqlDbType.UniqueIdentifier, (object)model.DatosFacturacionId ?? DBNull.Value));

            ExecuteNonQuery(cmd);

            return model;
        }

        public IEnumerable<Cliente> GetAll(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
