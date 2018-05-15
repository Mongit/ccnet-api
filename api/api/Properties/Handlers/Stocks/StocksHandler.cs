using System.Collections.Generic;
using BO.Stock;
using DAL;
using Microsoft.Extensions.Configuration;

namespace api.Properties.Handlers.Stocks
{
    public class StocksHandler : IStocksHandler
    {
        private IConfiguration Configuration { get; set; }
        IDAL<Stock> StocksDAL { get; set; }

        public StocksHandler(IConfiguration config, IDAL<Stock> stocksDAL)
        {
            Configuration = config;
            StocksDAL = stocksDAL;
        }

        public IEnumerable<Stock> GetAll(out int totalPages, int pageNumber, int pageSize)
        {
            return StocksDAL.GetAll(out totalPages, pageNumber, pageSize);
        }
    }
}
