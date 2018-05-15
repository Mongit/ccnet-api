using api.Properties.Handlers.Stocks;
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
                var stocks = StocksHandler.GetAll(out int totalPages, pageNumber, pageSize);

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
    }
}