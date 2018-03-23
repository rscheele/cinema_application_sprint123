using Domain.Entities;
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
        public ActionResult SelectSeats(List<Ticket> ticketList)
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult ChooseSeats()
        {
            List<Ticket> ticketList = (List<Ticket>)TempData["Tickets"];
            return RedirectToAction("Pay", "Pin", ticketList);
        }
    }
}