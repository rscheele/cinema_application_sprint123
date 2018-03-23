using Domain.Abstract;
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
        private IShowSeatRepository showSeatRepository;
        private IRoomRepository roomRepository;

        public SeatSelectionController(IShowSeatRepository showSeatRepository, IRoomRepository roomRepository)
        {
            this.showSeatRepository = showSeatRepository;
            this.roomRepository = roomRepository;
        }

        [HttpGet]
        public ActionResult SelectSeats()
        {
            List<Ticket> ticketList = (List<Ticket>)TempData["Tickets"];
            Show show = ticketList.FirstOrDefault().Show;
            Room room = roomRepository.GetRoom(show.RoomID);
            IEnumerable<ShowSeat> showSeats = showSeatRepository.GetShowSeats(show.ShowID);

            TempData["TicketList"] = ticketList;
            return View("SelectSeats", showSeats);
        }
        
        [HttpGet]
        public ActionResult ChooseSeats()
        {
            List<Ticket> ticketList = (List<Ticket>)TempData["Tickets"];
            return RedirectToAction("Pay", "Pin", ticketList);
        }
    }
}