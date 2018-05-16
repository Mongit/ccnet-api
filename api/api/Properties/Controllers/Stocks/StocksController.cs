using api.Properties.Handlers.Stocks;
using api.Properties.Models.Stocks;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Dynamic;

namespace api.Properties.Controllers.Stocks
{
    [Authorize]
    [Route("api/[controller]")]
    public class StocksController : Controller
    {
        private ILog Log { get; set; }
        private IStocksHandler StocksHandler { get; set; }

        public StocksController(ILog log, IStocksHandler stocksHandler)
        {
            Log = log;
            StocksHandler = stocksHandler;
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        public IActionResult Get(int pageNumber, int pageSize)
        {
            try
            {
                var stocks = StocksHandler.GetReport(out int totalPages, pageNumber, pageSize);

                dynamic objeto = new ExpandoObject();
                objeto.totalPages = totalPages;
                objeto.stocks = stocks;

                return new ObjectResult(objeto);
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] StockModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StocksHandler.Save(model.GetBusinessObject());
                    return new ObjectResult("Guardado exitosamente");
                }
                return new ObjectResult("Lo sentimos, hubo un error, revise los campos ingresados.");
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
            StocksHandler.Delete(id);
            return new ObjectResult("El registrp se ha eliminado exitosamente.");
        }
    }
}