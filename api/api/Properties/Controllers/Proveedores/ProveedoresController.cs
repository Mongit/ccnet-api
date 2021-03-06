﻿using api.Properties.Handlers.Proveedores;
using api.Properties.Models;
using BO.Proveedor;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Dynamic;

namespace api.Properties.Controllers.Proveedores
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProveedoresController : Controller
    {
        private ILog Log { get; set; }
        private IProveedoresHandler ProveedoresHandler { get; set; }

        public ProveedoresController(ILog log, IProveedoresHandler proveedoresHandler)
        {
            Log = log;
            ProveedoresHandler = proveedoresHandler;
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        public IActionResult Get(int pageNumber, int pageSize)
        {
            try
            {
                var proveedores = ProveedoresHandler.GetAll(out int totalPages, pageNumber, pageSize);

                dynamic objeto = new ExpandoObject();
                objeto.totalPages = totalPages;
                objeto.proveedores = proveedores;

                return new ObjectResult(objeto);
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProveedorModel model)
        {
            try
            {
                Guid savedId = ProveedoresHandler.Save(model.GetBusinessObject());
                return new ObjectResult(savedId);
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public Proveedor Get(Guid id)
        {
            try
            {
                return ProveedoresHandler.GetOne(id);
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpPut("{id}")]
        public string Put(Guid id, [FromBody]ProveedorModel model)
        {
            try
            {
                Proveedor modelUpdated = ProveedoresHandler.Update(id, model.GetBusinessObject());
                return string.Format("Proveedor modificado exitosamente");
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            ProveedoresHandler.Delete(id);
            return new ObjectResult("Proveedor eliminado exitosamente.");
        }

        [HttpGet("search/term/{searchTerm}")]
        public ActionResult Get(string searchTerm)
        {
            try
            {
                return new ObjectResult(ProveedoresHandler.SearchByTerm(searchTerm));
            }
            catch (Exception ex)
            {
                Log.Error(null, ex);
                throw ex;
            }
        }
    }
}