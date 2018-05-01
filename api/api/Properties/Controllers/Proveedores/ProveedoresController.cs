using api.Properties.Handlers.Proveedores;
using api.Properties.Models;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Dynamic;

namespace api.Properties.Controllers.Proveedores
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProveedoresController : Controller
    {
        private ILog Log { get; set; }
        private IProveedoresHandler ProveedoresHandler { get; set; }

        public ProveedoresController(ILog log, IProveedoresHandler proveedoresHandler)
        {
            Log = log;
            ProveedoresHandler = proveedoresHandler;
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        public IActionResult Get(int pageNumber, int pageSize)
        {
            try
            {
                var proveedores = ProveedoresHandler.GetAll(out int totalPages, pageNumber, pageSize);

                dynamic objeto = new ExpandoObject();
                objeto.totalPages = totalPages;
                objeto.proveedores = proveedores;

                return new ObjectResult(objeto);
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProveedorModel model)
        {
            try
            {
                Guid savedId = ProveedoresHandler.Save(model.GetBusinessObject());
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