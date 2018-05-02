using api.Properties.Handlers.Proveedores;
using api.Properties.Models.Proveedores;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace api.Properties.Controllers.Proveedores
{
    [Authorize]
    [Route("api/[controller]")]
    public class CuentasController : Controller
    {
        private ILog Log { get; set; }
        private ICuentasHandler CuentasHandler { get; set; }

        public CuentasController(ILog log, ICuentasHandler cuentasHandler)
        {
            Log = log;
            CuentasHandler = cuentasHandler;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return new ObjectResult(CuentasHandler.GetAll());
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CuentaModel model)
        {
            try
            {
                Guid savedId = CuentasHandler.Save(model.GetBusinessObject());
                return new ObjectResult(savedId);
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }
    }
}