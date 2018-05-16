using BO.Stock;
using System;
using System.Collections.Generic;

namespace api.Properties.Handlers.Stocks
{
    public interface IStocksHandler
    {
        IEnumerable<Stock> GetAll(out int totalPages, int pageNumber, int pageSize);
        Guid Save(Stock model);
    }
}
