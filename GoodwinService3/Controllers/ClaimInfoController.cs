using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GoodwinService3.Controllers
{
    public class ClaimInfoController : ApiController
    {
        // GET: api/ClaimInfo
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/ClaimInfo/5
        public object Get(int id)
        {
            object result;
            dynamic o = new { controller = "ClaimDocument", action = "Get" };
            int port;
            if (Url.Request.RequestUri.Host.StartsWith("gto-srv1.goodwin-soft.com"))
            {
                port = 8080;
            }
            else
            {
                port = Url.Request.RequestUri.Port;
            }

            Uri uriDoc = new Uri(string.Format("{0}://{1}:{2}{3}", Url.Request.RequestUri.Scheme, Url.Request.RequestUri.Host, port, Url.Route("Default", o)));
            //string uriDoc = string.Format("{0}://{1}:{2}{3}", Url.Request.RequestUri.Scheme, Url.Request.RequestUri.Host, Url.Request.RequestUri.Port, Url.Route("Default", o));
            try
            {
                result = new Models.ClaimInfo(id, uriDoc.ToString());
            }
            catch (Exception ex)
            {

                result = ex.Message;
            }
            
            return result;
        }

        // POST: api/ClaimInfo
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT: api/ClaimInfo/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/ClaimInfo/5
        //public void Delete(int id)
        //{
        //}
    }
}
