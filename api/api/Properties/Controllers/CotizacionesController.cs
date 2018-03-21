using api.Properties.Handlers;
using api.Properties.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace api.Properties.Controllers
{
    [Route("api/[controller]")]
    public class CotizacionesController : Controller
    {
        private ILog Log { get; set; }
        private ICotizacionesHandler CotizacionesHandler { get; set; }

        public CotizacionesController(ILog log, ICotizacionesHandler cotizacionesHandler)
        {
            Log = log;
            CotizacionesHandler = cotizacionesHandler;
        }


        [HttpPost]
        public IActionResult Post([FromBody] CotizacionModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Guid savedId = CotizacionesHandler.Save(model.GetBusinessObject());
                    return new ObjectResult(savedId);
                }
                return new ObjectResult(Guid.NewGuid());
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            try
            {
                return new string[] { "cotizacion1", "cotizacion2" };
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public IEnumerable<BO.Cotizacion> Get(Guid id)
        {
            return CotizacionesHandler.GetAll(id);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            CotizacionesHandler.Delete(id);
            return new ObjectResult(id);
        }
    }
}