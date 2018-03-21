using BO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class PresupuestosItemDAL : BaseApiDAL<PresupuestoItem>, IDAL<PresupuestoItem>
    {
        public PresupuestosItemDAL(IConfiguration config) : base("PresupuestoItems", config, "PresupuestoId")
        {
        }

        public override Guid Save(PresupuestoItem model)
        {
            SqlCommand cmd = NewCommand(SqlQueries.SAVE_SP, CommandType.StoredProcedure);

            cmd.Parameters.Add(GetParam("@id", SqlDbType.UniqueIdentifier, model.Id));
            cmd.Parameters.Add(GetParam("@cantidad", SqlDbType.Decimal, model.Cantidad));
            cmd.Parameters.Add(GetParam("@descripcion", SqlDbType.VarChar, model.Descripcion));
            cmd.Parameters.Add(GetParam("@precio", SqlDbType.Decimal, model.Precio));
            cmd.Parameters.Add(GetParam("@presupuestoId", SqlDbType.UniqueIdentifier, model.PresupuestoId));

            int count = ExecuteNonQuery(cmd);

            return model.Id;
        }

        public override PresupuestoItem Load(SqlDataReader dr)
        {
            PresupuestoItem it = new PresupuestoItem();
            it.Id = GetCastValue<Guid>(dr["Id"]);
            it.Cantidad = GetCastValue<decimal>(dr["Cantidad"]);
            it.Descripcion = GetString(dr["Descripcion"]);
            it.Precio = GetCastValue<decimal>(dr["Precio"]);
            it.PresupuestoId = GetCastValue<Guid>(dr["PresupuestoId"]);

            return it;
        }

        public override void SaveChildren(IEnumerable<PresupuestoItem> model)
        {
            if (model.Count() == 0) return;
            PresupuestoItem[] items = model.ToArray();
            Guid parentId = items[0].PresupuestoId;
            DeleteChildren(parentId);
            DataTable table = GetChildrenTable(parentId);

            foreach (PresupuestoItem item in items)
            {
                DataRow drItem = GetExistingRowOrNew(table, item.Id);
                drItem["Id"] = item.Id;
                drItem["Cantidad"] = item.Cantidad;
                drItem["Descripcion"] = item.Descripcion;
                drItem["Precio"] = item.Precio;
                drItem["PresupuestoId"] = item.PresupuestoId;
            }

            CopyToServer(table);
        }

        public IEnumerable<PresupuestoItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PresupuestoItem> GetAll(Guid id)
        {
            throw new NotImplementedException();
        }

        public PresupuestoItem Update(Guid id, PresupuestoItem model)
        {
            throw new NotImplementedException();
        }
    }
}
