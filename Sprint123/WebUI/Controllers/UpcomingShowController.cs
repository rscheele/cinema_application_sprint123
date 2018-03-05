using Domain.Abstract;
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

        public UpcomingShowController(IMovieOverviewRepository movieRepository, IShowRepository showRepository)
        {
            this.movieRepository = movieRepository;
            this.showRepository = showRepository;
        }

        // GET: UpcomingShow
        public ActionResult UpcomingShows()
        {

            DateTime now = DateTime.Now;
            DateTime EndOfDay = DateTime.Today.AddDays(1).AddHours(2); //needs a change  
        
            List<Show> allShows = showRepository.GetShows().ToList();

            //Filter out showings from the past
            List<Show> ShowsFromNow = allShows.ToEnumerable()
                .Where(s => s.BeginTime > now).ToList();



            //Order by show date -> newest first
            List<Show> ShowsFromNowOrderedByDate = ShowsFromNow.ToEnumerable()
                .OrderBy(s => s.BeginTime).ToList();

            //takes shows form current workday
            List<Show> ShowsFromNowOrderedTillEndDayByDate = ShowsFromNow.ToEnumerable()
                .Where(s => s.EndTime < EndOfDay).ToList();
            

            List<Movie> ShowsMovies = new List<Movie>();

            foreach (Show s in ShowsFromNowOrderedTillEndDayByDate)
            {
                ShowsMovies.Add(s.Movie);
            }

            DateTime today = DateTime.Now;
            string DayOfWeek = today.DayOfWeek.ToString();

            DateTime time = DateTime.Now;
            string HourOfDay = time.Hour.ToString();
            string MinuteOfDay = time.Minute.ToString();

            ViewBag.Movies = ShowsMovies;
            ViewBag.Shows = ShowsFromNowOrderedTillEndDayByDate;
            ViewBag.DayOfWeek = DayOfWeek;
            ViewBag.HourOfDay = HourOfDay;
            ViewBag.MinuteOfDay = MinuteOfDay;

            //TempData["model"] = model;

            //return View(model);

            return View();
        }
    }
}