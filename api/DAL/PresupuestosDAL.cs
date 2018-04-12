using BO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    public class PresupuestosDAL : BaseApiDAL<Presupuesto>, IDAL<Presupuesto>
    {
        public PresupuestosDAL(IConfiguration config, string v) : base("PRESUPUESTOS", config, "cotizacionId")
        {
        }

        public override Guid Save(Presupuesto model)
        {
            SqlCommand cmd = NewCommand(SqlQueries.SAVE_SP, CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, model.Id));
            cmd.Parameters.Add(GetParam("@cantidad", SqlDbType.Decimal, model.Cantidad));
            cmd.Parameters.Add(GetParam("@descripcion", SqlDbType.VarChar, model.Descripcion));
            cmd.Parameters.Add(GetParam("@porcentajeGastos", SqlDbType.Decimal, model.PorcentajeGastos));
            cmd.Parameters.Add(GetParam("@porcentajeGanancia", SqlDbType.Decimal, model.PorcentajeGanancia));
            cmd.Parameters.Add(GetParam("@porcentajeIVA", SqlDbType.Decimal, model.PorcentajeIVA));
            cmd.Parameters.Add(GetParam("@cotizacionId", SqlDbType.UniqueIdentifier, model.CotizacionId));
            ExecuteNonQuery(cmd);

            return model.Id;
        }

        public override Presupuesto Load(SqlDataReader dr)
        {
            Presupuesto it = new Presupuesto();
            it.Id = GetCastValue<Guid>(dr["Id"]);
            it.Cantidad = GetCastValue<decimal>(dr["Cantidad"]);
            it.Descripcion = GetString(dr["Descripcion"]);
            it.PorcentajeGastos = GetCastValue<decimal>(dr["PorcentajeGastos"]);
            it.PorcentajeGanancia = GetCastValue<decimal>(dr["PorcentajeGanancia"]);
            it.PorcentajeIVA = GetCastValue<decimal>(dr["PorcentajeIVA"]);
            it.CotizacionId = GetCastValue<Guid>(dr["CotizacionId"]);

            return it;
        }

        public override void SaveChildren(IEnumerable<Presupuesto> model)
        {
            if (model.Count() == 0) return;
            Presupuesto[] items = model.ToArray();
            Guid parentId = items[0].CotizacionId;
            DeleteChildren(parentId);
            DataTable table = GetChildrenTable(parentId);

            foreach (Presupuesto item in items)
            {
                DataRow drItem = GetExistingRowOrNew(table, item.Id);
                drItem["Id"] = item.Id;
                drItem["Cantidad"] = item.Cantidad;
                drItem["Descripcion"] = item.Descripcion;
                drItem["PorcentajeGastos"] = item.PorcentajeGastos;
                drItem["PorcentajeGanancia"] = item.PorcentajeGanancia;
                drItem["PorcentajeIVA"] = item.PorcentajeIVA;
                drItem["CotizacionId"] = item.CotizacionId;
            }

            CopyToServer(table);

        }

        public IEnumerable<Presupuesto> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Presupuesto> GetAll(Guid id)
        {
            throw new NotImplementedException();
        }

        public Presupuesto Update(Guid id, Presupuesto model)
        {
            SqlCommand cmd = NewCommand(SqlQueries.UPDATE_SP, CommandType.StoredProcedure);
            
            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, model.Id));
            cmd.Parameters.Add(GetParam("@cantidad", SqlDbType.Decimal, model.Cantidad));
            cmd.Parameters.Add(GetParam("@descripcion", SqlDbType.VarChar, model.Descripcion));
            cmd.Parameters.Add(GetParam("@porcentajeGastos", SqlDbType.Decimal, model.PorcentajeGastos));
            cmd.Parameters.Add(GetParam("@porcentajeGanancia", SqlDbType.Decimal, model.PorcentajeGanancia));
            cmd.Parameters.Add(GetParam("@porcentajeIVA", SqlDbType.Decimal, model.PorcentajeIVA));
            cmd.Parameters.Add(GetParam("@cotizacionId", SqlDbType.UniqueIdentifier, model.CotizacionId));
            
            ExecuteNonQuery(cmd);

            return model;
        }
    }
}
