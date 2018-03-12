using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ReservationController : Controller
    {
        private IMovieOverviewRepository movieRepository;
        private IShowRepository showRepository;
        private ITicketRepository ticketRepository;

        public ReservationController(IMovieOverviewRepository movieRepository, IShowRepository showRepository, ITicketRepository ticketRepository)
        {
            this.movieRepository = movieRepository;
            this.showRepository = showRepository;
            this.ticketRepository = ticketRepository;
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

                TempData["Tickets"] = tickets;
                return View("DisplayReservation", tickets);
            }
            else
            {
                return View("NoReservationFound");
            }
        }

        [HttpGet]
        public ActionResult PrintTickets()
        {
            List<Ticket> tickets = (List<Ticket>)TempData["Tickets"];
            var pdf = new PrintTickets(tickets);
            return pdf.SendPdf();
        }
    }
}