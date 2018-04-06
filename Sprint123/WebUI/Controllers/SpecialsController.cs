using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class SpecialsController : Controller
    {
        // GET: Specials
        public ActionResult Specials()
        {
            return View();
        }

        public ActionResult Ladies()
        {
            return View("Ladies");
        }
    }
}