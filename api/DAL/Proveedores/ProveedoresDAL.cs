using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BO.Proveedor;
using Microsoft.Extensions.Configuration;

namespace DAL.Proveedores
{
    public class ProveedoresDAL : BaseApiDAL<Proveedor>, IDAL<Proveedor>
    {
        public ProveedoresDAL(IConfiguration config, string parentCol = "") : base("PROVEEDORES", config, parentCol)
        {
        }

        public IEnumerable<Proveedor> GetAll(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Proveedor> GetAll(out int totalPages, int pageNumber = 1, int pageSize = 100)
        {
            try
            {
                using(SqlCommand cmd = new SqlCommand(SqlQueries.GET_ALL_SP))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pn = GetParam("@PageNumber", SqlDbType.Int, pageNumber);
                    SqlParameter ps = GetParam("@PageSize", SqlDbType.Int, pageSize);
                    SqlParameter tp = GetParam("@TotalPages", SqlDbType.Int, 0);
                    tp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pn);
                    cmd.Parameters.Add(ps);
                    cmd.Parameters.Add(tp);

                    List<Proveedor> list = new List<Proveedor>();
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

        public IEnumerable<Proveedor> GetAll(Guid id, out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Proveedor> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Proveedor Load(SqlDataReader dr)
        {
            Proveedor it = new Proveedor();
            it.Id = GetCastValue<Guid>(dr["Id"]);
            it.Empresa = GetString(dr["Empresa"]);
            it.Contacto = GetString(dr["Contacto"]);
            it.Domicilio = GetString(dr["Domicilio"]);
            it.Telefono = GetString(dr["Telefono"]);
            it.Email = GetString(dr["Email"]);
            it.HorarioAtencion = GetString(dr["HorarioAtencion"]);
            
            return it;
        }

        public override Guid Save(Proveedor model)
        {
            SqlCommand cmd = NewCommand(SqlQueries.SAVE_SP, CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, model.Id));
            cmd.Parameters.Add(GetParam("@empresa", SqlDbType.VarChar, model.Empresa));
            cmd.Parameters.Add(GetParam("@contacto", SqlDbType.VarChar, model.Contacto));
            cmd.Parameters.Add(GetParam("@domicilio", SqlDbType.VarChar, model.Domicilio));
            cmd.Parameters.Add(GetParam("@telefono", SqlDbType.VarChar, model.Telefono));
            cmd.Parameters.Add(GetParam("@email", SqlDbType.VarChar, model.Email));
            cmd.Parameters.Add(GetParam("@horarioAtencion", SqlDbType.VarChar, model.HorarioAtencion));

            ExecuteNonQuery(cmd);
            
            return model.Id; ;
        }

        public Proveedor Update(Guid id, Proveedor model)
        {
            SqlCommand cmd = NewCommand(SqlQueries.UPDATE_SP, CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, id));
            cmd.Parameters.Add(GetParam("@empresa", SqlDbType.VarChar, model.Empresa));
            cmd.Parameters.Add(GetParam("@contacto", SqlDbType.VarChar, model.Contacto));
            cmd.Parameters.Add(GetParam("@domicilio", SqlDbType.VarChar, model.Domicilio));
            cmd.Parameters.Add(GetParam("@telefono", SqlDbType.VarChar, model.Telefono));
            cmd.Parameters.Add(GetParam("@email", SqlDbType.VarChar, model.Email));
            cmd.Parameters.Add(GetParam("@horarioAtencion", SqlDbType.VarChar, model.HorarioAtencion));

            ExecuteNonQuery(cmd);

            return model;
        }
    }
}
