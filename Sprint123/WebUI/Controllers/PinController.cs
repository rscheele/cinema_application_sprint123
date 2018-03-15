using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class PinController : Controller
    {
        public PinController() { }

        // GET: Pin
        public ViewResult Pay()
        {
            List<Ticket> ticketsList = (List<Ticket>)TempData["TicketList"];
            //ViewBag.tickets = ticketsList;

            PinViewModel model = (PinViewModel)TempData["model"];
            if(model == null)
            {
                model = new PinViewModel();
            }
            TempData["model"] = model;
            TempData["ticketList"] = ticketsList;
            return View("Pay", model);
        }

        [HttpGet]
        public ActionResult PinViewAddNumber(String s)
        {
            PinViewModel model = (PinViewModel)TempData["model"];

            model.PinValue += s;

            TempData["model"] = model;
            return RedirectToAction("Pay");
        }

        [HttpGet]
        public ActionResult PinViewRemoveNumber()
        {
            PinViewModel model = (PinViewModel)TempData["model"];

            model.PinRemoveNumber();

            TempData["model"] = model;
            return RedirectToAction("Pay");
        }
    }
}