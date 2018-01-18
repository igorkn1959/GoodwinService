using GoodwinService3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace GoodwinService3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Help Page";
            ViewBag.Dir = HttpContext.Request.Url.Host + ":" + HttpContext.Request.Url.Port.ToString();

            return View();
        }
    }
}
