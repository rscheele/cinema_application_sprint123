﻿using Domain.Abstract;
using Domain.Entities;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class UpcomingShowController : Controller
    {
        private IMovieOverviewRepository movieRepository;
        private IShowRepository showRepository;
        private ITicketRepository ticketRepository;
        //need ticketRepo for secret movie;

        public UpcomingShowController(IMovieOverviewRepository movieRepository, IShowRepository showRepository, ITicketRepository ticketRepository)
        {
            this.movieRepository = movieRepository;
            this.showRepository = showRepository;
            this.ticketRepository = ticketRepository;
        }

        // GET: UpcomingShow
        public ActionResult Upcoming()
        {

            DateTime now = DateTime.Now;
            DateTime EndOfDay = DateTime.Today.AddDays(1) +new TimeSpan(02,00,00); //probably correct 
        
            List<Show> allShows = showRepository.GetShows().ToList();

            //Filter out showings from the past
            List<Show> ShowsFromNow = allShows.ToEnumerable()
                .Where(s => s.BeginTime > now).ToList();

            //List<Show> secretMovie = allShows.ToEnumerable().OrderBy(s => )

            //Order by show date -> newest first
            List<Show> ShowsFromNowOrderedByDate = ShowsFromNow.ToEnumerable()
                .OrderBy(s => s.BeginTime).ToList();

            //takes shows form current workday
            List<Show> upcomingShows = ShowsFromNowOrderedByDate.ToEnumerable()
                .Where(s => s.EndTime < EndOfDay).ToList();
            

            DateTime today = DateTime.Now;
            string DayOfWeek = today.DayOfWeek.ToString();

            DateTime time = DateTime.Now;
            string HourOfDay = time.Hour.ToString();
            string MinuteOfDay = time.Minute.ToString();
            string Location = "Cinemapolis";
            
            ViewBag.DayOfWeek = DayOfWeek;
            ViewBag.HourOfDay = HourOfDay;
            ViewBag.MinuteOfDay = MinuteOfDay;
            ViewBag.Location = Location;
            //will become upcomingShows
            return View(allShows);
        }

        public ActionResult OrderMovie(int id)
        {
            List<Show> allShows = showRepository.GetShows().ToList();
            Show orderedShow = allShows.Find(r => r.ShowID == id);
            TempData["Show"] = orderedShow;
            return RedirectToAction("OrderTickets", "Ticket");
        }
    }
}