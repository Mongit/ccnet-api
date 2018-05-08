using BO.Recibo;
using DAL;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace api.Properties.Handlers.Recibos
{
    public class RecibosHandler : IRecibosHandler
    {
        private IConfiguration Configuration { get; set; }
        IDAL<Recibo> RecibosDAL { get; set; }

        public RecibosHandler(IConfiguration config, IDAL<Recibo> recibosDAL)
        {
            Configuration = config;
            RecibosDAL = recibosDAL;
        }

        public IEnumerable<Recibo> GetAll(out int totalPages, int pageNumber, int pageSize)
        {
            return RecibosDAL.GetAll(out totalPages, pageNumber, pageSize);
        }

    }
}
