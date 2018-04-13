using BO;
using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace api.Properties.Handlers
{
    public class ClientesHandler : IClientesHandler
    {
        private IConfiguration Configuration { get; set; }
        IDAL<Cliente> ClientesDAL { get; set; }

        public ClientesHandler(IConfiguration config, IDAL<Cliente> clientesDal)
        {
            Configuration = config;
            ClientesDAL = clientesDal;
        }

        public Guid Save(Cliente model)
        {
            Guid savedId = ClientesDAL.Save(model);
            return savedId;
        }

        public IEnumerable<Cliente> GetAll(out int totalPages, int pageNumber, int pageSize)
        {
            return ClientesDAL.GetAll(out totalPages, pageNumber, pageSize);
        }

        public Cliente GetOne(Guid id)
        {
            return ClientesDAL.GetOne(id);
        }

        public Cliente Update(Guid id, Cliente model)
        {
            return ClientesDAL.Update(id, model);
        }
    }
}
