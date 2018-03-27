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
        private ITicketRepository ticketRepository;

        public PinController(ITempTicketRepository tempTicketRepository, ITicketRepository ticketRepository)
        {
            this.tempTicketRepository = tempTicketRepository;
            this.ticketRepository = ticketRepository;
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

        public ActionResult Finish(long reservationID)
        {
            List<TempTicket> tempTickets = tempTicketRepository.GetTempTicketsReservation(reservationID).ToList();
            bool paid = true;
            if (tempTickets.Count > 0)
            {
                foreach (var i in tempTickets)
                {
                    i.IsPaid = true;
                }
                tempTicketRepository.UpdateTempTickets(tempTickets);
                return RedirectToAction("EmailReservation", "Reservation", new { reservationID , paid});
            }
            else
            {
                List<Ticket> tickets = ticketRepository.GetTickets(reservationID).ToList();
                foreach (var i in tickets)
                {
                    i.IsPaid = true;
                }
                ticketRepository.UpdateTickets(tickets);
                return RedirectToAction("PrintReservationTickets", "Reservation", new { reservationID });
            }
        }
    }
}