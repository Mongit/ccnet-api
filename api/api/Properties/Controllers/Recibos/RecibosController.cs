using api.Properties.Handlers.Recibos;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Dynamic;

namespace api.Properties.Controllers.Recibos
{
    [Authorize]
    [Route("api/[controller]")]
    public class RecibosController : Controller
    {
        private ILog Log { get; set; }
        private IRecibosHandler RecibosHandler { get; set; }

        public RecibosController(ILog log, IRecibosHandler recibosHandler)
        {
            Log = log;
            RecibosHandler = recibosHandler;
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        public IActionResult Get(int pageNumber, int pageSize)
        {
            try
            {
                var recibos = RecibosHandler.GetAll(out int totalPages, pageNumber, pageSize);

                dynamic objeto = new ExpandoObject();
                objeto.totalPages = totalPages;
                objeto.recibos = recibos;

                return new ObjectResult(objeto);
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }
    }
}