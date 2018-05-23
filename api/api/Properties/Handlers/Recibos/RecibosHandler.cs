using BO.Recibo;
using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api.Properties.Handlers.Recibos
{
    public class RecibosHandler : IRecibosHandler
    {
        private IConfiguration Configuration { get; set; }
        IDAL<Recibo> RecibosDAL { get; set; }
        IDAL<ReciboItem> ReciboItemDAL { get; set; }
        IDAL<ReciboReport> RecibosReportDAL { get; set; }

        public RecibosHandler(IConfiguration config, IDAL<Recibo> recibosDAL, IDAL<ReciboItem> recibosItemDal, IDAL<ReciboReport> recibosReportDal)
        {
            Configuration = config;
            RecibosDAL = recibosDAL;
            ReciboItemDAL = recibosItemDal;
            RecibosReportDAL = recibosReportDal;
        }

        public IEnumerable<ReciboReport> GetReport(out int totalPages, int pageNumber, int pageSize)
        {
            return RecibosReportDAL.GetReport(out totalPages, pageNumber, pageSize);
        }

        public Guid Save(Recibo model)
        {
            return RecibosDAL.Save(model);
        }

        public Recibo GetOne(Guid id)
        {
            Recibo recibo = RecibosDAL.GetOne(id);
            IEnumerable<ReciboItem> items = ReciboItemDAL.GetChildren(id);
            recibo.Items = items.ToList();
            return recibo;
        }

        public void Update(Guid id, Recibo model)
        {
            RecibosDAL.Update(id, model);
        }

        public void SaveChildren(IEnumerable<ReciboItem> items)
        {
            ReciboItemDAL.SaveChildren(items);
        }

        public void Delete(Guid id)
        {
            ReciboItemDAL.DeleteChildren(id);
            RecibosDAL.Delete(id);
        }

        public IEnumerable<Recibo> SearchByTerm(string term)
        {
            return RecibosDAL.SearchByTerm(term);
        }
    }
}
