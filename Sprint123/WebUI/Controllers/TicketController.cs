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
        private IShowRepository showRepository;
        private ITempTicketRepository tempTicketRepository;
        private ITicketRepository ticketRepository;

        public TicketController(IShowRepository showRepository, ITempTicketRepository tempTicketRepository, ITicketRepository ticketRepository)
        {
            this.showRepository = showRepository;
            this.tempTicketRepository = tempTicketRepository;
            this.ticketRepository = ticketRepository;
        }

        [HttpGet]
        public ActionResult OrderTickets()
        {
            Show selectedShow = (Show)TempData["Show"];
            string soldOut = (string)TempData["SoldOut"];
            List<decimal> tarrifs = calculatePrices(selectedShow);
            //hide name if movie is secret ----BEGIN
            //Boolean IsSecret = (Boolean)TempData["Secret"];
            bool secret = (bool)TempData["Secret"];
            if (secret != true)
            {
                ViewBag.MovieName = selectedShow.Movie.Name;
            }
            else
            {
                ViewBag.MovieName = "?";
            }
            //hide name if movie is secret ----END
            ViewBag.NormalPrice = tarrifs[0];
            ViewBag.ChildPrice = tarrifs[1];
            ViewBag.StudentPrice = tarrifs[2];
            ViewBag.SeniorPrice = tarrifs[3];
            if (soldOut != null)
            {
                ViewBag.SoldOut = soldOut;
            }

            TempData["Show"] = selectedShow;
            TempData["Secret"] = secret;
            return View("OrderTickets");
        }

        [HttpPost]
        public ActionResult OrderTickets(Order order)
        {
            // Add a maximum of tickets that can be ordered based on database
            Show selectedShow = (Show)TempData["Show"];
            int bookedTickets = ticketRepository.GetShowTickets(selectedShow.ShowID).Count();
            int reservedTickets = tempTicketRepository.GetTempTicketsShow(selectedShow.ShowID).Count();
            int totalBookedSeats = bookedTickets + reservedTickets;
            int maxSeats = selectedShow.Room.TotalSeats;
            int seatsLeft = maxSeats - totalBookedSeats;
            int max = 10;
            if (seatsLeft < 10)
            {
                max = seatsLeft;
            }
            TempData["Show"] = selectedShow;

            TempData["Order"] = order;
            int ticketcount = order.studentTickets + order.seniorTickets + order.normalTickets + order.childTickets;
            if (max == 0)
            {
                TempData["SoldOut"] = "De show is helaas uitverkocht. Er zijn geen tickets meer beschikbaar.";
                return RedirectToAction("OrderTickets");
            }
            else if (ticketcount <= 0 | ticketcount > 10)
            {
                return RedirectToAction("OrderTickets");
            }
            else if (ticketcount > max)
            {
                TempData["SoldOut"] = "De show is bijna uitverkocht, er zijn nog maar " + max + " tickets beschikbaar!";
                return RedirectToAction("OrderTickets");
            }
            else
            {
                return RedirectToAction("AddPopcorn");
            }
            // return RedirectToAction("AddPopcorn");
        }

        [HttpGet]
        public ActionResult AddPopcorn()
        {
            Order order = (Order)TempData["Order"];
            Show selectedShow = (Show)TempData["Show"];
            List<decimal> tarrifs = calculatePrices(selectedShow);
            List<Ticket> tickets = new List<Ticket>();
            List<TempTicket> tempTickets = new List<TempTicket>();
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
                ticket.TicketType = "Standaard";
                tickets.Add(ticket);
            }
            // Add child tickets
            for (int i = 0; i < order.childTickets; i++)
            {
                Ticket ticket = new Ticket();
                ticket.Price = tarrifs[1];
                ticket.TicketType = "Kind";
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
                item.ShowID = selectedShow.ShowID;
                //adding seat data ---- BEGIN
                //item.Seat.SeatNumber =12;
                //item.Seat.RowY =2;
                //adding seat data ---- END
                item.ReservationID = reservationID;
                //Concurrency
                TempTicket tempTicket = new TempTicket();
                tempTicket.ReservationID = item.ReservationID;
                tempTicket.SeatID = item.SeatID;
                tempTicket.ShowID = item.ShowID;
                tempTicket.TimeAdded = DateTime.Now;
                tempTickets.Add(tempTicket);
                numberoftickets++;
            }
            selectedShow.NumberofTickets = selectedShow.NumberofTickets + numberoftickets;
            showRepository.SaveShow(selectedShow);
            tempTicketRepository.SaveTempTickets(tempTickets);

            TempData["Order"] = order;
            TempData["Show"] = selectedShow;
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
            //return RedirectToAction("SeatSelection", "SeatSelection", ticketList);
            return RedirectToAction("SelectSeats", "SeatSelection", ticketList);
        }

        public List<decimal> calculatePrices(Show show)
        {
            decimal normal;
            decimal child;
            decimal student;
            decimal senior;

            bool secret = (bool)TempData["Secret"];

            // Calculate the base price
            if (show.Movie.Length >= 120)
            {
                normal = 10.00M;
            } else
            {
                normal = 9.50M;
            }

            if (secret == true)
            {
                normal = normal - 2.50M;
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
            TempData["Secret"] = secret;
            return tariffs;
        }

    }
}