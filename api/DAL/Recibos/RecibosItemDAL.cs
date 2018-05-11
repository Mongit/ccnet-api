﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BO.Recibo;
using Microsoft.Extensions.Configuration;

namespace DAL.Recibos
{
    public class RecibosItemDAL : BaseApiDAL<ReciboItem>, IDAL<ReciboItem>
    {
        public RecibosItemDAL(IConfiguration config) : base("ReciboItems", config, "ReciboId")
        {
        }

        public IEnumerable<ReciboItem> GetAll(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReciboItem> GetAll(out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReciboItem> GetAll(Guid id, out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public override ReciboItem Load(SqlDataReader dr)
        {
            throw new NotImplementedException();
        }

        public override Guid Save(ReciboItem model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReciboItem> SearchByTerm(string term)
        {
            throw new NotImplementedException();
        }

        public ReciboItem Update(Guid id, ReciboItem model)
        {
            throw new NotImplementedException();
        }

        public override void SaveChildren(IEnumerable<ReciboItem> model) {
            if (model.Count() == 0) return;
            ReciboItem[] items = model.ToArray();
            Guid parentId = items[0].ReciboId;
            DeleteChildren(parentId);
            DataTable table = GetChildrenTable(parentId);

            foreach (ReciboItem item in items)
            {
                DataRow drItem = GetExistingRowOrNew(table, item.Id);
                drItem["Id"] = item.Id;
                drItem["Cantidad"] = item.Cantidad;
                drItem["Descripcion"] = item.Descripcion;
                drItem["Precio"] = item.Precio;
                drItem["ReciboId"] = item.ReciboId;
                drItem["CotizacionId"] = item.CotizacionId;
            }

            CopyToServer(table);
        }
    }
}