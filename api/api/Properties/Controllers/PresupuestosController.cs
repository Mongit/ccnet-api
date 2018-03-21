using api.Properties.Handlers;
using api.Properties.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System;

namespace api.Properties.Controllers
{
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
    }
}