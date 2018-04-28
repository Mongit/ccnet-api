using BO;
using DAL;
using System;

namespace api.Properties.Handlers
{
    public interface IUsuariosHandler
    {
        Usuario Authenticate(Usuario model);
    }

    public class UsuariosHandler : IUsuariosHandler
    {
        UsuariosDAL UsuariosDAL { get; set; }

        public UsuariosHandler(UsuariosDAL UsuariosDal)
        {
            UsuariosDAL = UsuariosDal;
        }

        public Usuario Authenticate(Usuario model)
        {
            return UsuariosDAL.Authenticate(model);
        }
    }
}
