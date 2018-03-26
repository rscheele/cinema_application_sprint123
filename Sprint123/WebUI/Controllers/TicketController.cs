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
        public ActionResult OrderTickets(long reservationID, int showID)
        {
            Show selectedShow = showRepository.FindShow(showID);
            string soldOut = (string)TempData["SoldOut"];
            List<decimal> tarrifs = calculatePrices(selectedShow);

            // Set reservation ID for order
            Order order = new Order();
            order.showID = showID;
            order.reservationID = reservationID;

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

            TempData["Secret"] = secret;
            return View("OrderTickets", order);
        }

        [HttpPost]
        public ActionResult OrderTickets(Order order)
        {
            // Add a maximum of tickets that can be ordered based on database
            Show selectedShow = showRepository.FindShow(order.showID);
            List<TempTicket> tempTickets = new List<TempTicket>();
            int bookedTickets = ticketRepository.GetShowTickets(selectedShow.ShowID).Count();
            int reservedTickets = tempTicketRepository.GetTempTicketsShow(selectedShow.ShowID).Count();
            int totalBookedSeats = bookedTickets + reservedTickets;
            int maxSeats = selectedShow.Room.TotalSeats;
            int seatsLeft = maxSeats - totalBookedSeats;
            int max = 10;
            int numberoftickets = 0;
            if (seatsLeft < 10)
            {
                max = seatsLeft;
            }
            TempData["Show"] = selectedShow;

            //TempData["Order"] = order;
            // Check if there are tickets available
            int ticketcount = order.studentTickets + order.seniorTickets + order.normalTickets + order.childTickets;
            if (max == 0)
            {
                TempData["SoldOut"] = "De show is helaas uitverkocht. Er zijn geen tickets meer beschikbaar.";
                return RedirectToAction("OrderTickets", new { order.reservationID });
            }
            else if (ticketcount <= 0 | ticketcount > 10)
            {
                return RedirectToAction("OrderTickets", new { order.reservationID });
            }
            else if (ticketcount > max)
            {
                TempData["SoldOut"] = "De show is bijna uitverkocht, er zijn nog maar " + max + " tickets beschikbaar!";
                return RedirectToAction("OrderTickets", new { order.reservationID });
            }
            else
            {
                List<decimal> tarrifs = calculatePrices(selectedShow);
                // Add normal tickets
                for (int i = 0; i < order.normalTickets; i++)
                {
                    TempTicket tempTicket = new TempTicket();
                    tempTicket.Price = tarrifs[0];
                    tempTicket.TicketType = "Standaard";
                    tempTicket.ReservationID = order.reservationID;
                    tempTicket.ShowID = selectedShow.ShowID;
                    tempTicket.TimeAdded = DateTime.Now;
                    tempTicket.Show = selectedShow;
                    tempTicket.IsPaid = false;
                    tempTicket.ShowID = selectedShow.ShowID;
                    tempTickets.Add(tempTicket);
                    numberoftickets++;
                }
                // Add child tickets
                for (int i = 0; i < order.childTickets; i++)
                {
                    TempTicket tempTicket = new TempTicket();
                    tempTicket.Price = tarrifs[1];
                    tempTicket.TicketType = "Kind";
                    tempTicket.ReservationID = order.reservationID;
                    tempTicket.ShowID = selectedShow.ShowID;
                    tempTicket.TimeAdded = DateTime.Now;
                    tempTicket.Show = selectedShow;
                    tempTicket.IsPaid = false;
                    tempTicket.ShowID = selectedShow.ShowID;
                    tempTickets.Add(tempTicket);
                    numberoftickets++;
                }
                // Add student tickets
                for (int i = 0; i < order.studentTickets; i++)
                {
                    TempTicket tempTicket = new TempTicket();
                    tempTicket.Price = tarrifs[2];
                    tempTicket.TicketType = "Student";
                    tempTicket.ReservationID = order.reservationID;
                    tempTicket.ShowID = selectedShow.ShowID;
                    tempTicket.TimeAdded = DateTime.Now;
                    tempTicket.Show = selectedShow;
                    tempTicket.IsPaid = false;
                    tempTicket.ShowID = selectedShow.ShowID;
                    tempTickets.Add(tempTicket);
                    numberoftickets++;
                }
                // Add senior tickets
                for (int i = 0; i < order.seniorTickets; i++)
                {
                    TempTicket tempTicket = new TempTicket();
                    tempTicket.Price = tarrifs[3];
                    tempTicket.TicketType = "Senior";
                    tempTicket.ReservationID = order.reservationID;
                    tempTicket.ShowID = selectedShow.ShowID;
                    tempTicket.TimeAdded = DateTime.Now;
                    tempTicket.Show = selectedShow;
                    tempTicket.IsPaid = false;
                    tempTicket.ShowID = selectedShow.ShowID;
                    tempTickets.Add(tempTicket);
                    numberoftickets++;
                }
                selectedShow.NumberofTickets = selectedShow.NumberofTickets + numberoftickets;
                tempTicketRepository.SaveTempTickets(tempTickets);
                return RedirectToAction("AddPopcorn", new { order.reservationID });
            }
        }

        [HttpGet]
        public ActionResult AddPopcorn(long reservationID)
        {
            List<TempTicket> tempTickets = tempTicketRepository.GetTempTicketsReservation(reservationID).ToList();
            return View("AddPopcorn", tempTickets);
        }

        [HttpPost]
        public ActionResult AddPopcorn(List<TempTicket> tickets)
        {
            List<TempTicket> ticketList = tempTicketRepository.GetTempTicketsReservation(tickets.FirstOrDefault().ReservationID).ToList();
            for (int i = 0; i < tickets.Count; i++)
            {
                if (tickets[i].Popcorn == true)
                {
                    ticketList[i].Price = ticketList[i].Price + 5M;
                    ticketList[i].Popcorn = true;
                }
                else
                {
                    ticketList[i].Popcorn = false;
                }
            }           
            return RedirectToAction("SelectSeats", "SeatSelection", new { reservationID = tickets.FirstOrDefault().ReservationID });
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