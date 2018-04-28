using api.Properties.Handlers;
using api.Properties.Models;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace api.Properties.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration _config;
        private ILog Log { get; set; }
        private IUsuariosHandler UsuariosHandler { get; set; }

        public TokenController(IConfiguration config, ILog log, IUsuariosHandler usuariosHandler)
        {
            _config = config;
            Log = log;
            UsuariosHandler = usuariosHandler;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody] UsuarioModel model)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(model);

            if(user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        private UsuarioModel Authenticate(UsuarioModel model)
        {
            BO.Usuario usuarioBo = UsuariosHandler.Authenticate(model.GetBusinessObject());
            if (usuarioBo == null)
            {
                return null;
            }
            return model;
        }

        private string BuildToken(UsuarioModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}