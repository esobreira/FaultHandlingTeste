﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FaultHandling.Controllers
{
    public class DefaultController : ApiController
    {
        // GET: api/Default/5
        public IHttpActionResult Get(string cep)
        {
            return Json(CorreiosService.ConsultarCep(cep));
        }

        // POST: api/Default
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Default/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Default/5
        public void Delete(int id)
        {
        }
    }
}
