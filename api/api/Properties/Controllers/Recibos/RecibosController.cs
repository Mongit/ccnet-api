using api.Properties.Handlers.Recibos;
using api.Properties.Models.Recibos;
using BO.Recibo;
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

        [HttpPost]
        public IActionResult Post([FromBody] ReciboModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Guid savedId = RecibosHandler.Save(model.GetBusinessObject());
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

        [HttpGet("{id}")]
        public Recibo Get(Guid id)
        {
            try
            {
                return RecibosHandler.GetOne(id);
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpPut("{id}")]
        public string Put(Guid id, [FromBody]ReciboModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var reciboBo = model.GetBusinessObject();
                    RecibosHandler.SaveChildren(reciboBo.Items);
                    RecibosHandler.Update(id, reciboBo);

                    return string.Format("Producto guardado exitosamente.");
                }
                return string.Format("Lo sentimos, el producto no se pudo guardar, le pedimos revise los datos ingresados.");
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }
    }
}