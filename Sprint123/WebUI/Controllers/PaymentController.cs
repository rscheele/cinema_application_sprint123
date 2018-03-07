using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        [HttpGet]
        public ActionResult Payment()
        {
            return View("Payment");
        }

        [HttpGet]
        public ActionResult Succes()
        {
            return View("Succes");
        }
    }
}