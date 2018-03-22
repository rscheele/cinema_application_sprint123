using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ReservationController : Controller
    {
        private IMovieOverviewRepository movieRepository;
        private IShowRepository showRepository;
        private ITicketRepository ticketRepository;
        private ITempTicketRepository tempTicketRepository;

        public ReservationController(IMovieOverviewRepository movieRepository, IShowRepository showRepository, ITicketRepository ticketRepository, ITempTicketRepository tempTicketRepository)
        {
            this.movieRepository = movieRepository;
            this.showRepository = showRepository;
            this.ticketRepository = ticketRepository;
            this.tempTicketRepository = tempTicketRepository;
        }

        // GET: Reservation
        [HttpGet]
        public ActionResult Reservation()
        {
            return View("Reservation");
        }

        [HttpPost]
        public ActionResult Reservation(string reservationID)
        {
            long resID = Convert.ToInt64(reservationID);
            IEnumerable<Ticket> tickets = ticketRepository.GetTickets(resID);

            if (tickets.Count() > 0) { 
                // Add the show to the ticket
                List<Show> allShows = showRepository.GetShows().ToList();
                Show orderedShow = allShows.Find(r => r.ShowID == tickets.First().ShowID);
                foreach (var item in tickets)
                {
                    item.Show = orderedShow;
                }

                TempData["TicketList"] = tickets;
                if (tickets.First().IsPaid == true)
                {
                    return View("DisplayReservation", tickets);
                }
                else
                {
                    return RedirectToAction("Pay", "Pin", tickets);
                }
            }
            else
            {
                return View("NoReservationFound");
            }
        }

        [HttpGet]
        public ActionResult PrintTickets()
        {
            PinViewModel model = (PinViewModel)TempData["model"];

            if (model.PinValue == "")
            {
                model.IncorrectPinValue = "Vul pincode in";

                TempData["model"] = model;
                return RedirectToAction("Pay", "Pin");
            }

            if (model.PinValue == "0000" | model.PinValue.Length <= 3)
            {
                model.IncorrectPinValue = "Vul een geldige pincode in";

                TempData["model"] = model;
                return RedirectToAction("Pay", "Pin");
            }
            else
            {
                List<Ticket> tickets = (List<Ticket>)TempData["TicketList"];
                foreach (var item in tickets)
                {
                    item.IsPaid = true;
                }
                ticketRepository.SaveTickets(tickets);
                tempTicketRepository.DeleteTempTickets(tickets.FirstOrDefault().ReservationID);
                var pdf = new PrintTickets(tickets);
                return pdf.SendPdf(); 
            }
        }

        [HttpGet]
        public ActionResult PrintReservationTickets()
        {
            IEnumerable<Ticket> ticketss = (IEnumerable<Ticket>)TempData["TicketList"];
            List<Ticket> tickets = ticketss.ToList();
            var pdf = new PrintTickets(tickets);
            return pdf.SendPdf();
        }
    }
}