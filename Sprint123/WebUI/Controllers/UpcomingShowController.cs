using Domain.Abstract;
using Domain.Entities;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class UpcomingShowController : Controller
    {
        private IMovieOverviewRepository movieRepository;
        private IShowRepository showRepository;
        //need ticketRepo for secret movie;

        public UpcomingShowController(IMovieOverviewRepository movieRepository, IShowRepository showRepository)
        {
            this.movieRepository = movieRepository;
            this.showRepository = showRepository;
        }

        public String GetFilmBase64String(Movie movie)
        {
            var base64 = Convert.ToBase64String(movie.Image);
            return String.Format("data:image/gif;base64,{0}", base64);
        }
        // GET: Upcoming2
        //[HttpGet]
        public ActionResult Upcoming2(int locationid, string searchString/*, bool? x, bool? xx, bool? xxx, bool? xxxx, bool? xxxxx*/)
        {
            int Locationid = locationid;
            DateTime now = DateTime.Now;
            //DateTime EndOfWeek = now.AddDays(5);
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysUntilThursday = ((int)DayOfWeek.Thursday - (int)now.DayOfWeek + 7) % 7;
            DateTime nextThursday = now.AddDays(daysUntilThursday);

            List<Show> allShows = showRepository.GetShows().ToList();
            //Filter out shows from different location
            List<Show> allThislocationShows = allShows.ToEnumerable().Where(s => s.Movie.LocationID == Locationid).ToList();
            //Filter out shows from the past
            List<Show> ShowsFromNow = allThislocationShows.ToEnumerable()
                .Where(s => s.BeginTime > now).ToList();
            //Order by show date
            List<Show> ShowsFromNowOrderedByDate = ShowsFromNow.ToEnumerable()
                .OrderBy(s => s.BeginTime).ToList();
            //take shows form current movie week
            List<Show> upcomingShows = ShowsFromNowOrderedByDate.ToEnumerable()
                .Where(s => s.EndTime < nextThursday).ToList();        
            //--------------filters--------------------
            if (!String.IsNullOrEmpty(searchString))
            {
                List <Show> filteredShows = allShows.ToEnumerable().Where(s => s.Movie.Name.Contains(searchString)).ToList();
                return View(filteredShows);

            }
            if(!DateTime searchDate){
                List <Show> filteredShows
            }
            //--------------filters--------------------
            //will become upcomingShows
            //ViewBag.locationId = Locationid;
            //TempData["location"] = Locationid;
            //TempData["upcomingShows"] = allShows;
            return View(allShows);
            //return View(upcomingShows);
        }

        // GET: UpcomingShow
        [HttpGet]
        public ActionResult Upcoming(int locationid)
        {
            
            int Locationid = locationid; 
            DateTime now = DateTime.Now;
            DateTime EndOfDay = DateTime.Today.AddDays(1) +new TimeSpan(02,00,00);
        
            List<Show> allShows = showRepository.GetShows().ToList();

            List<Show> allThislocationShows = allShows.ToEnumerable().Where(s => s.Movie.LocationID == Locationid).ToList();
            
            //Filter out shows from the past
            List<Show> ShowsFromNow = allShows.ToEnumerable()
                .Where(s => s.BeginTime > now).ToList();

            //Order by show date
            List<Show> ShowsFromNowOrderedByDate = ShowsFromNow.ToEnumerable()
                .OrderBy(s => s.BeginTime).ToList();

            //take shows form current workday
            List<Show> upcomingShows = ShowsFromNowOrderedByDate.ToEnumerable()
                .Where(s => s.EndTime < EndOfDay).ToList();

            //--secret movie ---
            IEnumerable<Show> showx = allShows;
            //IEnumerable<Show> list = showRepository.GetShows();
            IEnumerable<Show> secretShow = showx.OrderBy(s => s.NumberofTickets).Take(1);
            Show show = secretShow.First();
            String showid = show.ShowID.ToString();
            string begintime = show.BeginTime.ToString();
            string endtime = show.EndTime.ToString();
            string language = show.Movie.Language.ToString();
            string sublanguage = show.Movie.LanguageSub.ToString();
            string length = show.Movie.Length.ToString();
            string room = show.RoomID.ToString();
            int age = show.Movie.Age;

            ViewBag.showid = showid;
            ViewBag.begintime = begintime;
            ViewBag.endtime = endtime;
            ViewBag.threed = show.Movie.Is3D;
            ViewBag.language = language;
            ViewBag.sublanguage = sublanguage;
            ViewBag.length = length;
            ViewBag.age = age;
            ViewBag.room = room;
            //--secret movie ---         
            DateTime today = DateTime.Now;
            var culture = new System.Globalization.CultureInfo("nl-NL");
            var day = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);

            string ParsedDayOfWeek = day;

            DateTime time = DateTime.Now;
            string HourOfDay = time.Hour.ToString();
            string MinuteOfDay = time.Minute.ToString();
            string Location = show.Movie.Location.Name;//location name
            
            ViewBag.DayOfWeek = ParsedDayOfWeek;
            ViewBag.HourOfDay = HourOfDay;
            ViewBag.MinuteOfDay = MinuteOfDay;
            ViewBag.Location = Location;
            //will become upcomingShows
            return View(allShows);
            //return View(upcomingShows);
        }

        [HttpGet]
        public ActionResult ShowDetails(int id)
        {
            List<Show> allShows = showRepository.GetShows().ToList();
            Show orderedShow = allShows.Find(r => r.ShowID == id);
            return View("ShowDetails", orderedShow);

        }

        [HttpGet]
        public ActionResult OrderMovie(int id, bool secret)
        {
            List<Show> allShows = showRepository.GetShows().ToList();
            Show orderedShow = allShows.Find(r => r.ShowID == id);
            TempData["Show"] = orderedShow;
            TempData["Secret"] = secret;
            return RedirectToAction("OrderTickets", "Ticket");
        }
    }
}