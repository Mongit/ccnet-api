using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BO.Proveedor;
using Microsoft.Extensions.Configuration;

namespace DAL.Proveedores
{
    public class CuentasDAL : BaseApiDAL<Cuenta>, IDAL<Cuenta>
    {
        public CuentasDAL(IConfiguration config, string parentCol = "") : base("CUENTAS", config, parentCol)
        {
        }

        public IEnumerable<Cuenta> GetAll(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cuenta> GetAll()
        {
            SqlCommand cmd = new SqlCommand("SELECT * from Cuentas");

            List<Cuenta> list = new List<Cuenta>();
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

        public IEnumerable<Cuenta> GetAll(out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cuenta> GetAll(Guid id, out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public override Cuenta Load(SqlDataReader dr)
        {
            Cuenta it = new Cuenta();
            it.Id = GetCastValue<Guid>(dr["Id"]);
            it.ProveedorId = GetCastValue<Guid>(dr["ProveedorId"]);
            it.Banco = GetCastValue<string>(dr["Banco"]);
            it.Titular = GetCastValue<string>(dr["Titular"]);
            it.CLABE = GetCastValue<string>(dr["CLABE"]);
            it.NoCuenta = GetCastValue<string>(dr["NoCuenta"]);

            return it;
        }

        public override Guid Save(Cuenta model)
        {
            throw new NotImplementedException();
        }

        public Cuenta Update(Guid id, Cuenta model)
        {
            throw new NotImplementedException();
        }
    }
}
