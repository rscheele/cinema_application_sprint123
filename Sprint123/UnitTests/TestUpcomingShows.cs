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
            //List<Movie> movieList = new List<Movie>{
            //    new Movie
            //    {
            //        MovieID = 1,
            //        Name = "Black Panther",
            //        Language = "Engels",
            //        LanguageSub = "Nederlands",
            //        Age = 16,
            //        MovieType = 1,
            //        Length = 120,
            //        Is3D = true,
            //        LocationID = 1
            //    },
            //    new Movie
            //    {
            //        MovieID = 2,
            //        Name="Red Sparrow",
            //        Language ="Engels",
            //        LanguageSub ="Nederlands",
            //        Age = 16,
            //        MovieType = 2,
            //        Length = 140,
            //        Is3D = false,
            //        LocationID = 1
            //    },
            //    new Movie
            //    {
            //        MovieID=3,
            //        Name = "Three BillBoards Outside Ebbing, Missouri",
            //        Language = "Engels",
            //        LanguageSub ="Nederlands",
            //        Age = 16,
            //        MovieType = 2,
            //        Length = 115,
            //        Is3D = false,
            //        LocationID =1
            //    }
            //};
            List<Show> showlist = new List<Show> {
                new Show //third
                {
                    ShowID = 1,
                    BeginTime = new DateTime(2018, 3, 13, 20, 15, 00),
                    EndTime   = new DateTime(2018, 3, 13, 22, 15, 00),
                    MovieID = 1,
                    NumberofTickets = 20,
                    ChildDiscount = false,
                    StudentDiscount = true,
                    SeniorDiscount = true,
                },
                new Show //second
                {
                    ShowID = 2,
                    BeginTime = new DateTime(2018, 3, 13, 17, 30, 00),
                    EndTime   = new DateTime(2018, 3, 14, 19, 30, 00),
                    MovieID = 2,
                    NumberofTickets = 10,
                    ChildDiscount = false,
                    StudentDiscount = true,
                    SeniorDiscount = true,
                },
                new Show //first
                {
                    ShowID = 3,
                    BeginTime = new DateTime(2018, 3, 13, 15, 15, 00),
                    EndTime   = new DateTime(2018, 3, 14, 17, 15, 00),
                    MovieID = 3,
                    NumberofTickets = 10,
                    ChildDiscount = false,
                    StudentDiscount = true,
                    SeniorDiscount = true,

                }
            };

            //arrange

            //var mockRepo = new Mock<IMovieOverviewRepository>();
            //var mockRepo2 = new Mock<IShowRepository>();
            //var mockRepo3 = new Mock<ITicketRepository>();
            //UpcomingShowController controller = new UpcomingShowController(mockRepo.Object, mockRepo2.Object, mockRepo3.Object);

            DateTime now = DateTime.Now;
            DateTime EndOfDay = DateTime.Today.AddDays(1) + new TimeSpan(02, 00, 00);


            List<Show> allShows = showlist;

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
                allShows[0].ShowID,
                allShows[1].ShowID,
                allShows[2].ShowID
            };
            
            List<int> ExpectedIDsofShows = new List<int>
            {
                1,
                2,
                3
            };
            // Assert -- assert if expected equals given by act.
            Assert.IsTrue(ShowIDIsEqual(IdOfShows, ExpectedIDsofShows));
        }
    }
}
