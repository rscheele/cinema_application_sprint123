using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject.Infrastructure.Language;
using WebUI.Controllers;

namespace UnitTests
{
    [TestClass]
    public class TestUpcomingShows
    {
        
        private bool ShowIDIsEqual(List<int> l1, List<int> l2)
        {
            if (l1.Count != l2.Count)
            {
                return false;
            }

            for (int i = 0; i < l1.Count; i++)
            {
                if (l1[i] != l2[i])
                {
                    return false;
                }
            }

            return true;
        }

        [TestMethod]
        public void TestForUpcomingShows() {
            //IMPORTANT: test only works before 19:15 hours. !!!
            //arrange
            DateTime show1Begintime = DateTime.Today+new TimeSpan(19, 15, 00);
            DateTime show1Endtime = DateTime.Today + new TimeSpan(21, 15, 00);

            DateTime show2Begintime = DateTime.Today + new TimeSpan(21, 30, 00);
            DateTime show2Endtime = DateTime.Today + new TimeSpan(22, 30, 00);

            DateTime show3Begintime = DateTime.Today + new TimeSpan(22, 45, 00);
            DateTime show3Endtime = DateTime.Today.AddDays(1) + new TimeSpan(0, 15, 00);

            List<Show> showlist = new List<Show> {
                new Show //third
                {
                    ShowID = 1,
                    BeginTime = show3Begintime,
                    EndTime   = show3Endtime,
                    MovieID = 1,
                    NumberofTickets = 20,
                    ChildDiscount = false,
                    StudentDiscount = true,
                    SeniorDiscount = true,
                },
                new Show //second
                {
                    ShowID = 2,
                    BeginTime = show2Begintime,
                    EndTime   = show2Endtime,
                    MovieID = 2,
                    NumberofTickets = 10,
                    ChildDiscount = false,
                    StudentDiscount = true,
                    SeniorDiscount = true,
                },
                new Show //first
                {
                    ShowID = 3,
                    BeginTime = show1Endtime,
                    EndTime   = show1Endtime,
                    MovieID = 3,
                    NumberofTickets = 10,
                    ChildDiscount = false,
                    StudentDiscount = true,
                    SeniorDiscount = true,

                }
            };

            
            DateTime now = DateTime.Now;
            DateTime EndOfDay = DateTime.Today.AddDays(1) + new TimeSpan(02, 00, 00);


            List<Show> allShows = showlist;
            //Act
            //Filter out shows from the past
            List<Show> ShowsFromNow = allShows.ToEnumerable()
                .Where(s => s.BeginTime > now).ToList();

            //Order by show date
            List<Show> ShowsFromNowOrderedByDate = ShowsFromNow.ToEnumerable()
                .OrderBy(s => s.BeginTime).ToList();

            //take shows form current workday
            List<Show> upcomingShows = ShowsFromNowOrderedByDate.ToEnumerable()
                .Where(s => s.EndTime < EndOfDay).ToList();
       
            List<int> IdOfShows = new List<int>
            {
                upcomingShows[0].ShowID,
                upcomingShows[1].ShowID,
                upcomingShows[2].ShowID
            };
            
            List<int> ExpectedIDsofShows = new List<int>
            {
                3,
                2,
                1
            };
            // Assert -- assert if expected equals given by act.
            Assert.IsTrue(ShowIDIsEqual(IdOfShows, ExpectedIDsofShows));
        }

        [TestMethod]
        public void TestFiltersForMovies()
        {
            //arrange
            //TO DO add movie data
            string search = "Bruce Willis";
            string genre = "actie";
            string genre2 = "kids";
            List<Movie> movielist = new List<Movie> {
                new Movie
                {
                    //actie film met bruce willis
                },
                new Movie
                {
                    //actie film
                },
                new Movie
                {
                    //avontuur film
                },
                new Movie
                {
                    //kids film
                },
                new Movie
                {
                    //kids film
                }
            };

            List<Movie> allMovies = movielist;
            //Act

            //TO DO: add filter query's
            List<Movie> filteredSearchMovies = allMovies.ToEnumerable()
                    .Where(s => s.Name.Contains(search)
                    || s.MainActors.Contains(search)
                    || s.SubActors.Contains(search)
                    || s.Director.Contains(search))
                    .ToList();

            List<Movie> filteredMovies = allMovies.ToEnumerable()
                   .Where(s => s.Genre.ToString() == genre)
                   .ToList();

            List<Movie> filteredMovies2 = allMovies.ToEnumerable()
                   .Where(s => s.Genre.ToString() == genre2)
                   .ToList();
            

            List<int> IdOfFilteredMovies = new List<int>
            {

            };

            List<int> IdOfFilteredMovies2 = new List<int>
            {

            };

            List<int> ExpectedIDsofFilteredMovies = new List<int>
            {
                3,
                2,
                1
            };
            List<int> ExpectedIDsofFilteredMovies2 = new List<int>
            {
                3,
                2,
                1
            };
            // Assert -- assert if expected equals given by act.
            Assert.IsTrue(ShowIDIsEqual(IdOfFilteredMovies, ExpectedIDsofFilteredMovies));
            Assert.IsTrue(ShowIDIsEqual(IdOfFilteredMovies2, ExpectedIDsofFilteredMovies2));
        }
    }
}
