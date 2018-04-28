using Newtonsoft.Json;

namespace api.Properties.Models
{
    public class UsuarioModel
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "contrasena")]
        public string Contrasena { get; set; }

        public BO.Usuario GetBusinessObject()
        {
            BO.Usuario usuario = new BO.Usuario
            {
                Email = Email,
                Contrasena = Contrasena
            };

            return usuario;
        }
    }
}
