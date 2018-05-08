﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BO.Recibo;
using Microsoft.Extensions.Configuration;

namespace DAL.Recibos
{
    public class RecibosDAL : BaseApiDAL<Recibo>, IDAL<Recibo>
    {
        public RecibosDAL(IConfiguration config, string parentCol = "") : base("RECIBOS", config, parentCol)
        {
        }

        public IEnumerable<Recibo> GetAll(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recibo> GetAll(out int totalPages, int pageNumber, int pageSize)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GET_ALL_SP))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pn = GetParam("@PageNumber", SqlDbType.Int, pageNumber);
                    SqlParameter ps = GetParam("@PageSize", SqlDbType.Int, pageSize);
                    SqlParameter tp = GetParam("@TotalPages", SqlDbType.Int, 0);
                    tp.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pn);
                    cmd.Parameters.Add(ps);
                    cmd.Parameters.Add(tp);

                    List<Recibo> list = new List<Recibo>();
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

        public IEnumerable<Recibo> GetAll(Guid id, out int totalPages, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public override Recibo Load(SqlDataReader dr)
        {
            Recibo it = new Recibo();
            it.Id = GetCastValue<Guid>(dr["Id"]);
            it.Folio = GetCastValue<int>(dr["Folio"]);
            it.ClienteId = GetCastValue<Guid>(dr["ClienteId"]);
            it.ProveedorId = GetCastValue<Guid>(dr["ProveedorId"]);
            it.Fecha = GetCastValue<DateTime>(dr["Fecha"]);
            
            return it;
        }

        public override Guid Save(Recibo model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recibo> SearchByTerm(string term)
        {
            throw new NotImplementedException();
        }

        public Recibo Update(Guid id, Recibo model)
        {
            throw new NotImplementedException();
        }
    }
}
