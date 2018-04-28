using BO;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace DAL
{
    public class UsuariosDAL : BaseDAL
    {
        public UsuariosDAL(IConfiguration config) : base(config)
        {
        }

        public Usuario Load(SqlDataReader dr)
        {
            Usuario it = new Usuario
            {
                Email = GetString(dr["Email"]),
                Contrasena = GetString(dr["Contrasena"])
            };

            return it;
        }

        public Usuario Authenticate (Usuario model)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * from Usuarios where Email = '" + model.Email +
                    "' AND Contrasena = '" + model.Contrasena + "'");

                Usuario it = default(Usuario);
                Action<SqlDataReader> action = (dr =>
                {
                    if (dr.Read())
                    {
                        it = Load(dr);
                    }
                });
                ExecuteDataReader(cmd, action);

                return it;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
