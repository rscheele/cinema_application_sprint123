using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class TicketController : Controller
    {
        private ITicketRepository ticketRepository;
        private IShowRepository showRepository;

        public TicketController(ITicketRepository ticketRepository, IShowRepository showRepository)
        {
            this.ticketRepository = ticketRepository;
            this.showRepository = showRepository;
        }

        [HttpGet]
        public ActionResult OrderTickets()
        {
            Show selectedShow = (Show)TempData["Show"];
            List<decimal> tarrifs = calculatePrices(selectedShow);

            ViewBag.MovieName = selectedShow.Movie.Name;
            ViewBag.NormalPrice = tarrifs[0];
            ViewBag.ChildPrice = tarrifs[1];
            ViewBag.StudentPrice = tarrifs[2];
            ViewBag.SeniorPrice = tarrifs[3];

            TempData["Show"] = selectedShow;
            return View("OrderTickets");
        }

        [HttpPost]
        public ActionResult OrderTickets(Order order)
        {
            TempData["Order"] = order;
            return RedirectToAction("AddPopcorn"); ;
        }

        [HttpGet]
        public ActionResult AddPopcorn()
        {
            Order order = (Order)TempData["Order"];
            Show selectedShow = (Show)TempData["Show"];
            List<decimal> tarrifs = calculatePrices(selectedShow);
            List<Ticket> tickets = new List<Ticket>();
            int numberoftickets = 0;
            // Generate ReservationID for reservation based on date and time
            DateTime dateTime = DateTime.Now;
            int year = dateTime.Year;
            int doy = dateTime.DayOfYear;
            int hour = dateTime.Hour;
            int minute = dateTime.Minute;
            int ms = dateTime.Millisecond;
            long reservationID = long.Parse(year.ToString() + doy.ToString().PadLeft(3, '0') + hour.ToString().PadLeft(2, '0') + minute.ToString().PadLeft(2, '0') + ms.ToString().PadLeft(3, '0'));

            // Add normal tickets
            for (int i = 0; i < order.normalTickets; i++)
            {
                Ticket ticket = new Ticket();
                ticket.Price = tarrifs[0];
                ticket.TicketType = "Normal";
                tickets.Add(ticket);
            }
            // Add child tickets
            for (int i = 0; i < order.childTickets; i++)
            {
                Ticket ticket = new Ticket();
                ticket.Price = tarrifs[1];
                ticket.TicketType = "Child";
                tickets.Add(ticket);
            }
            // Add student tickets
            for (int i = 0; i < order.studentTickets; i++)
            {
                Ticket ticket = new Ticket();
                ticket.Price = tarrifs[2];
                ticket.TicketType = "Student";
                tickets.Add(ticket);
            }
            // Add senior tickets
            for (int i = 0; i < order.seniorTickets; i++)
            {
                Ticket ticket = new Ticket();
                ticket.Price = tarrifs[3];
                ticket.TicketType = "Senior";
                tickets.Add(ticket);
            }
            // Set the other values
            foreach (var item in tickets)
            {
                item.Show = selectedShow;
                item.IsPaid = false;
                item.Popcorn = false;
                item.SeatID = 0;
                item.ReservationID = reservationID;
                numberoftickets++;
            }
            selectedShow.NumberofTickets = selectedShow.NumberofTickets + numberoftickets;
            showRepository.SaveShow(selectedShow);

            TempData["Tickets"] = tickets;
            return View("AddPopcorn", tickets);
        }

        [HttpPost]
        public ActionResult AddPopcorn(List<Ticket> tickets)
        {
           
            List<Ticket> ticketList = (List<Ticket>)TempData["Tickets"];
            for (int i = 0; i < tickets.Count; i++)
            {
                if (tickets[i].Popcorn == true)
                {
                    ticketList[i].Price = ticketList[i].Price + 5M;
                    ticketList[i].Popcorn = true;
                }
            }           
            TempData["TicketList"] = ticketList;
            return RedirectToAction("SummaryView", "Summary", ticketList);
        }

        public List<decimal> calculatePrices(Show show)
        {
            decimal normal;
            decimal child;
            decimal student;
            decimal senior;

            // Calculate the base price
            if (show.Movie.Length >= 120)
            {
                normal = 10.00M;
            } else
            {
                normal = 9.50M;
            }

            // Calculate wether the movie is in 3D
            if (show.Movie.Is3D == true)
            {
                normal = normal + 2.50M;
            }

            // Calculate child tarrif
            if (show.BeginTime.Hour < 18 && show.Movie.Language == "Nederlands")
            {
                child = normal - 1.75M;
            } else
            {
                child = normal;
            }

            // Calculate student tarrif
            if (show.BeginTime.DayOfWeek == DayOfWeek.Monday | show.BeginTime.DayOfWeek == DayOfWeek.Tuesday | show.BeginTime.DayOfWeek == DayOfWeek.Wednesday | show.BeginTime.DayOfWeek == DayOfWeek.Thursday)
            {
                student = normal - 1.50M;
            }
            else
            {
                student = normal;
            }

            // Calculate senior tarrif
            // Holidays NYI
            if (show.BeginTime.DayOfWeek == DayOfWeek.Monday | show.BeginTime.DayOfWeek == DayOfWeek.Tuesday | show.BeginTime.DayOfWeek == DayOfWeek.Wednesday | show.BeginTime.DayOfWeek == DayOfWeek.Thursday)
            {
                senior = normal - 1.50M;
            }
            else
            {
                senior = normal;
            }

            List<decimal> tariffs = new List<decimal>();
            tariffs.Add(normal);
            tariffs.Add(child);
            tariffs.Add(student);
            tariffs.Add(senior);
            return tariffs;
        }

    }
}