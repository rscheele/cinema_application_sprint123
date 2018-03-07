using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class TicketController : Controller
    {
        [HttpGet]
        public ActionResult OrderTickets()
        {
            return View("OrderTickets");
        }

        [HttpGet]
        public ActionResult AddPopcorn()
        {
            return View("AddPopcorn");
        }
    }
}