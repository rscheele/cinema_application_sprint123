using Domain.Abstract;
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
        private ITempTicketRepository tempTicketRepository;

        public PinController(ITempTicketRepository tempTicketRepository)
        {
            this.tempTicketRepository = tempTicketRepository;
        }

        // GET: Pin
        public ViewResult Pay(long reservationID)
        {
            List<TempTicket> ticketsList = tempTicketRepository.GetTempTicketsReservation(reservationID).ToList();
            //ViewBag.tickets = ticketsList;

            PinViewModel model = (PinViewModel)TempData["model"];
            if(model == null)
            {
                model = new PinViewModel();
            }
            model.reservationID = reservationID;
            TempData["model"] = model;
            return View("Pay", model);
        }

        [HttpGet]
        public ActionResult PinViewAddNumber(String s, long reservationID)
        {
            PinViewModel model = (PinViewModel)TempData["model"];

            model.PinValue += s;

            TempData["model"] = model;
            return RedirectToAction("Pay", new { reservationID });
        }

        [HttpGet]
        public ActionResult PinViewRemoveNumber(long reservationID)
        {
            PinViewModel model = (PinViewModel)TempData["model"];

            model.PinRemoveNumber();

            TempData["model"] = model;
            return RedirectToAction("Pay", new { reservationID });
        }
    }
}