using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sindicato_v1.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult UnauthorizedOperation()
        {
            ViewBag.msjeErrorExcepcion = "No tiene permisos";
            return View();
        }

        public ActionResult InaccessiblePage()
        {
            return View();
        }
    }
}