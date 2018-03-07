using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;

namespace WebUI.Controllers
{
    public class TicketController : Controller
    {
        [HttpGet]
        public ActionResult OrderTickets()
        {
            Show selectedShow = (Show)TempData["Show"];
            

            return View("OrderTickets");
        }

        [HttpGet]
        public ActionResult AddPopcorn()
        {
            return View("AddPopcorn");
        }
    }
}