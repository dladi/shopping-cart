using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PricingService.Controllers
{
    public class PricingController : ApiController
    {
        // GET: api/Pricing
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Pricing/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Pricing
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Pricing/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Pricing/5
        public void Delete(int id)
        {
        }
    }
}
