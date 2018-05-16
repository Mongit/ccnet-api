using System;
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
        IDAL<StockReport> StocksReportDAL { get; set; }

        public StocksHandler(IConfiguration config, IDAL<Stock> stocksDAL, IDAL<StockReport> stocksReportDAL)
        {
            Configuration = config;
            StocksDAL = stocksDAL;
            StocksReportDAL = stocksReportDAL;
        }

        public IEnumerable<StockReport> GetReport(out int totalPages, int pageNumber, int pageSize)
        {
            return StocksReportDAL.GetReport(out totalPages, pageNumber, pageSize);
        }

        public Guid Save(Stock model)
        {
            return StocksDAL.Save(model);
        }

        public void Delete(Guid id)
        {
            StocksDAL.Delete(id);
        }
    }
}
