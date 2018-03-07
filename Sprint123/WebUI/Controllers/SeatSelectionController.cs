using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class SeatSelectionController : Controller
    {
        [HttpGet]
        public ActionResult SelectSeats()
        {
            return View();
        }
    }
}