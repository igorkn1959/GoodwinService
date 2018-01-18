using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GoodwinService3.Controllers
{
    [Authorize]
    public class BronClaimController : ApiController
    {
        
        public Models.BronClaim Get(string catclaim)
        {
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            var list = Properties.Settings.Default.IpList;
            if (list.Contains(ip))
            {
                Models.BronClaim bron;
                try
                {
                    bron = new Models.BronClaim(catclaim);
                    bron.FillPacket();

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
                return bron;
            }
            else
            {
                return null;
            }

        }

        // GET: api/BronClaim/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/BronClaim
        public Models.BronClaim Post(Models.BronClaim bron, string action)//, string action ) //[FromBody]string bron
        {
            Debug.Assert(bron != null, "Bron == NULL");
            //string result = "";
            switch (action)
            {
                case "CALC":
                    bron.CalckBron(false);
                    //result = "Calc OK";
                    break;
                case "SAVE":
                    bron.CalckBron(true);
                    //result = "Save OK";
                    break;

                default:
                    break;
            }
            return bron;
        }

        // PUT: api/BronClaim/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/BronClaim/5
        //public void Delete(int id)
        //{
        //}
    }
}
