using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ConfirmationController : Controller
    {
        // GET: Confirmation
        public ActionResult Confirm()
        {
            return View();
        }
    }
}