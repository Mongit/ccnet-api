using api.Properties.Handlers;
using api.Properties.Models;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace api.Properties.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PresupuestosController : Controller
    {
        private ILog Log { get; set; }
        private IPresupuestosHandler PresupuestosHandler { get; set; }

        public PresupuestosController(ILog log, IPresupuestosHandler presupuestosHandler)
        {
            Log = log;
            PresupuestosHandler = presupuestosHandler;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PresupuestoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var presupuestoBo = model.GetBusinessObject();
                    PresupuestosHandler.SaveChildren(presupuestoBo.Items);
                    Guid savedId = PresupuestosHandler.Save(presupuestoBo);
                    return new ObjectResult(savedId);
                }
                return new ObjectResult(model);
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public IEnumerable<BO.Presupuesto> Get(Guid id)
        {
            try
            {
                return PresupuestosHandler.GetAll(id);
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpPut("{id}")]
        public string Put(Guid id, [FromBody]PresupuestoModel model)
        {
            try
            {
                BO.Presupuesto modelUpdated = PresupuestosHandler.Update(id, model.GetBusinessObject());
                return string.Format("Se modifico exitosamente: {0}, value = {1}", id, modelUpdated);
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            PresupuestosHandler.Delete(id);
            return new ObjectResult(id);
        }
    }
}