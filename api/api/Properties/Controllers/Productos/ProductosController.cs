using api.Properties.Handlers.Productos;
using api.Properties.Models.Productos;
using BO.Producto;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Dynamic;

namespace api.Properties.Controllers.Productos
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductosController : Controller
    {
        private ILog Log { get; set; }
        private IProductosHandler ProductosHandler { get; set; }

        public ProductosController(ILog log, IProductosHandler productosHandler)
        {
            Log = log;
            ProductosHandler = productosHandler;
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        public IActionResult Get(int pageNumber, int pageSize)
        {
            try
            {
                var productos = ProductosHandler.GetAll(out int totalPages, pageNumber, pageSize);

                dynamic objeto = new ExpandoObject();
                objeto.totalPages = totalPages;
                objeto.productos = productos;

                return new ObjectResult(objeto);
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductoModel model)
        {
            try
            {
                ProductosHandler.Save(model.GetBusinessObject());
                return new ObjectResult("El producto se ha creado exitosamente.");
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public Producto Get(Guid id)
        {
            try
            {
                return ProductosHandler.GetOne(id);
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpPut("{id}")]
        public string Put(Guid id, [FromBody]ProductoModel model)
        {
            try
            {
                ProductosHandler.Update(id, model.GetBusinessObject());
                return string.Format("Producto guardado exitosamente.");
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
            try
            {
                ProductosHandler.Delete(id);
                return new ObjectResult("El producto se eliminó exitosamente.");
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpGet("search/term/{searchTerm}")]
        public ActionResult Get(string searchTerm)
        {
            try
            {
                return new ObjectResult(ProductosHandler.SearchByTerm(searchTerm));
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpGet("Get/Report/{pageNumber}/{pageSize}")]
        public IActionResult GetReport(int pageNumber, int pageSize)
        {
            try
            {
                var report = ProductosHandler.GetReport(out int totalPages, pageNumber, pageSize);

                dynamic objeto = new ExpandoObject();
                objeto.totalPages = totalPages;
                objeto.productos = report;

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