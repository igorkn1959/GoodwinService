using GoodwinService3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GoodwinService3.Controllers
{
    //[Authorize]
    public class ClaimCancelController : ApiController
    {
        // GET: api/ClaimCancel
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/ClaimCancel/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/ClaimCancel
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ClaimCancel/5
        public CancelClaim Put(int id, [FromBody]CancelClaim claim)
        {
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            var list = Properties.Settings.Default.IpList;
            if (list.Contains(ip))
            {
                claim.Cancel(id);
                return claim;

            }
            else
            {
                claim.result = "Error";
                claim.Error = "AccessDenied";
                //claim.mess.Add("Error", "AccessDenied");
                return claim;
            }
            
        }

        //// DELETE: api/ClaimCancel/5
        //public void Delete(int id)
        //{
        //}
    }
}
