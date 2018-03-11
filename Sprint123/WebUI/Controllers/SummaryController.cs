using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using Domain.Concrete;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class SummaryController : Controller
    {
        // GET: Summary
        public ActionResult SummaryView()
        {
            List<Ticket> ticketList = (List<Ticket>)TempData["TicketList"];
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            var context = new EFDbContext();

            var occupiedSeats = new List<Seat>();
            var ass = new AutomaticSeatSelection();


            foreach (Seat s in context.Seats.ToList())
            {

                Ticket t = context.Tickets.ToList().Find(ti => ti.SeatID == s.SeatID && ti.ShowID == model.SelectedShow.ShowID);

                if (t == null)
                {
                    continue;
                }

                if (s.SeatID != t.SeatID)
                {
                    continue;
                }
                occupiedSeats.Add(s);
            }

            if (model.SeatCoordList.Count == 0)
            {
                model.SeatCoordList = ass.CalculateSeats(model.SelectedShow.Room, model.GetAllTicketsQuantity(), occupiedSeats);
            }

            model.SeatSelectionGUI = ass.VisualizeSeats(model.SelectedShow.Room, occupiedSeats, model.SeatCoordList);

            TempData["model"] = model;
            return View("SummaryView", model);
        }

    }
   }
}