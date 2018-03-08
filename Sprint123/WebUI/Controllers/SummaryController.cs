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
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            var context = new EFDbContext();

            var occupiedSeats = new List<Seat>();
            var ass = new AutomaticSeatSelection();


            return View("SummaryView", model);
        
    }
   }
}