using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace GoodwinService3.Controllers
{
    public class ClaimDocumentController : Controller
    {
        // GET: api/ClaimDocument
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/ClaimDocument/5
        public FileStreamResult Get(int id)
        {
            kompasEntities db = new kompasEntities();
            var doc = db.external_document.Find(id);
            FileStreamResult result = null;
            if (doc != null)
            {
                MemoryStream stream = new MemoryStream(doc.content, 0, doc.content.Length);
                //stream.Read(doc.content, 0, doc.content.Length);
                result = new FileStreamResult(stream, doc.mime_type);
                result.FileDownloadName = doc.FileName;
                Response.AppendHeader("Connection", "keep-alive");
            }
            else
            {
                Response.StatusCode = 404;
            }
            return result;
        }

        // POST: api/ClaimDocument
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT: api/ClaimDocument/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/ClaimDocument/5
        //public void Delete(int id)
        //{
        //}
    }
}
