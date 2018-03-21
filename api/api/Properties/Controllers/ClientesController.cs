using api.Properties.Handlers;
using api.Properties.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace api.Properties.Controllers
{
    [Route("api/[controller]")]
    public class ClientesController : Controller
    {
        private ILog Log { get; set; }
        private IClientesHandler ClientesHandler { get; set; }

        public ClientesController(ILog log, IClientesHandler clientesHandler)
        {
            Log = log;
            ClientesHandler = clientesHandler;
        }
        // GET 
        [HttpGet]
        public IEnumerable<BO.Cliente> Get()
        {
            try
            {
                return ClientesHandler.GetAll();
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        //GET /id
        [HttpGet("{id}")]
        public BO.Cliente Get(Guid id)
        {
            try
            {
                return ClientesHandler.GetOne(id);
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        // POST
        [HttpPost]
        public IActionResult Post([FromBody] ClienteModel model)
        {
            try
            {
                Guid savedId = ClientesHandler.Save(model.GetBusinessObject());
                return new ObjectResult(savedId);
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        // PUT /id
        [HttpPut("{id}")]
        public string Put(Guid id, [FromBody]ClienteModel model)
        {
            try
            {
                BO.Cliente x = ClientesHandler.Update(id, model.GetBusinessObject());
                return string.Format("Se modifico exitosamente: {0}, value = {1}", id, model.Contacto);
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        // DELETE /id
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return "Hola desde Delete " + id;
        }
    }
}