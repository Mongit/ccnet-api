﻿using BO;
using System;
using System.Collections.Generic;

namespace api.Properties.Handlers
{
    public interface IClientesHandler
    {
        Guid Save(Cliente model);
        IEnumerable<Cliente> GetAll();
        Cliente GetOne(Guid id);
        Cliente Update(Guid id, Cliente model);
    }
}