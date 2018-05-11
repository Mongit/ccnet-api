using BO.Recibo;
using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace api.Properties.Handlers.Recibos
{
    public class RecibosHandler : IRecibosHandler
    {
        private IConfiguration Configuration { get; set; }
        IDAL<Recibo> RecibosDAL { get; set; }
        IDAL<ReciboItem> ReciboItemDAL { get; set; }

        public RecibosHandler(IConfiguration config, IDAL<Recibo> recibosDAL, IDAL<ReciboItem> recibosItemDal)
        {
            Configuration = config;
            RecibosDAL = recibosDAL;
            ReciboItemDAL = recibosItemDal;
        }

        public IEnumerable<Recibo> GetAll(out int totalPages, int pageNumber, int pageSize)
        {
            return RecibosDAL.GetAll(out totalPages, pageNumber, pageSize);
        }

        public Guid Save(Recibo model)
        {
            return RecibosDAL.Save(model);
        }

        public Recibo GetOne(Guid id)
        {
            return RecibosDAL.GetOne(id);
        }

        public void Update(Guid id, Recibo model)
        {
            RecibosDAL.Update(id, model);
        }

        public void SaveChildren(IEnumerable<ReciboItem> items)
        {
            ReciboItemDAL.SaveChildren(items);
        }
    }
}
