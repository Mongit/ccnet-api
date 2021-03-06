﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BO.Recibo;
using Microsoft.Extensions.Configuration;

namespace DAL.Recibos
{
    public class RecibosReportDAL : BaseApiDAL<ReciboReport>, IDAL<ReciboReport>
    {
        public RecibosReportDAL(IConfiguration config, string parentCol = "") : base("RECIBOS", config, parentCol)
        {
        }

        public IEnumerable<ReciboReport> GetAll(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReciboReport> GetAll(out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReciboReport> GetAll(Guid id, out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ReciboReport GetOneReport(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReciboReport> GetRange(int from, int to)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReciboReport> GetReport(out int totalPages, int pageNumber, int pageSize)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(SqlQueries.REPORT_SP))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pn = GetParam("@PageNumber", SqlDbType.Int, pageNumber);
                    SqlParameter ps = GetParam("@PageSize", SqlDbType.Int, pageSize);
                    SqlParameter tp = GetParam("@TotalPages", SqlDbType.Int, 0);
                    tp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pn);
                    cmd.Parameters.Add(ps);
                    cmd.Parameters.Add(tp);

                    List<ReciboReport> list = new List<ReciboReport>();
                    Action<SqlDataReader> action = (dr =>
                    {
                        while (dr.Read())
                        {
                            list.Add(Load(dr));
                        }
                    });

                    ExecuteDataReader(cmd, action);

                    totalPages = (int)tp.Value;

                    return list;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override ReciboReport Load(SqlDataReader dr)
        {
            ReciboReport it = new ReciboReport();
            it.Id = GetCastValue<Guid>(dr["Id"]);
            it.Folio = GetCastValue<int>(dr["Folio"]);
            it.ClienteId = GetCastValue<Guid>(dr["ClienteId"]);
            it.ProveedorId = GetCastValue<Guid>(dr["ProveedorId"]);
            it.Fecha = GetCastValue<DateTime>(dr["Fecha"]);
            it.ClienteName = GetString(dr["ClienteName"]);
            it.ProveedorName = GetString(dr["ProveedorName"]);

            return it;
        }

        public override Guid Save(ReciboReport model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReciboReport> SearchByTerm(string term)
        {
            throw new NotImplementedException();
        }

        public ReciboReport Update(Guid id, ReciboReport model)
        {
            throw new NotImplementedException();
        }
    }
}
