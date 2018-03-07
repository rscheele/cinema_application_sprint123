using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class CinemaInitializer : System.Data.Entity.DropCreateDatabaseAlways<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            //    SEQUENCE:
            //
            //    1. location, 
            //    2. movie 
            //    3. roomlayout
            //    4. room 
            //    5. seat 
            //    6. show
            //    7. ticket

            /* -----------start of table creation------------ */
            var locations = new List<Location>
            {
            new Location{Name="Cinemapolis",City="Breda", Rooms=6,TicketPrice=10.00M},
            };

            locations.ForEach(s => context.Locations.Add(s));
            context.SaveChanges();
            /* -----------end of table creation------------ */

            /* -----------start of table creation------------ */
            var movies = new List<Entities.Movie>
            {
            new Entities.Movie{Name="Darkest Hour",Language="Engels",LanguageSub="Nederlands", Age=16,MovieType=2,Length=120,Is3D=false,LocationID=1},
            new Entities.Movie{Name="Red Sparrow",Language="Engels",LanguageSub="Nederlands",Age=16,MovieType=2,Length=140,Is3D=false,LocationID=1},
            new Entities.Movie{Name="Death Wish",Language="Engels",LanguageSub="Nederlands",Age=16,MovieType=2,Length=108,Is3D=true,LocationID=1},
            new Entities.Movie{Name="Diep in de Zee",Language="Nederlands",LanguageSub="Nederlands",Age=6,MovieType=2,Length=91,Is3D=false,LocationID=1},
            new Entities.Movie{Name="Black Panther",Language="Engels",LanguageSub="Nederlands",Age=12,MovieType=2,Length=134,Is3D=true,LocationID=1},
            new Entities.Movie{Name="Bankier van het Verzet",Language="Nederlands",LanguageSub="Nederlands",Age=12,MovieType=2,Length=123,Is3D=false,LocationID=1},
            new Entities.Movie{Name="The Shape of Water",Language="Engels",LanguageSub="Nederlands",Age=16,MovieType=2,Length=123,Is3D=false,LocationID=1},
            new Entities.Movie{Name="Three BillBoards Outside Ebbing, Missouri",Language="Engels",LanguageSub="Nederlands",Age=16,MovieType=2,Length=115,Is3D=false,LocationID=1},
            new Entities.Movie{Name="De grote blije kinderfilm",Language="Nederlands",LanguageSub="Nederlands",Age=4,MovieType=2,Length=150,Is3D=false,LocationID=1}
            };

            movies.ForEach(s => context.Movies.Add(s));
            context.SaveChanges();
            /* -----------end of table creation------------ */

            /* -----------start of table creation------------ */
            var roomlayouts = new List<RoomLayout>
            {
            new RoomLayout{FrontX=20, FrontY=20, BackX=2, BackY=10},
            new RoomLayout{FrontX=15, FrontY=15, BackX=1, BackY=10}
            };

            roomlayouts.ForEach(s => context.RoomLayouts.Add(s));
            context.SaveChanges();
            /* -----------end of table creation------------ */

            /* -----------start of table creation------------ */
            var rooms = new List<Room>
            {
            new Room{RoomNumber=1,LayoutID=1,LocationID=1},
            new Room{RoomNumber=2,LayoutID=1,LocationID=1},
            new Room{RoomNumber=3,LayoutID=2,LocationID=1},
            new Room{RoomNumber=4,LayoutID=2,LocationID=1},
            new Room{RoomNumber=5,LayoutID=2,LocationID=1},
            new Room{RoomNumber=6,LayoutID=2,LocationID=1}
            };

            rooms.ForEach(s => context.Rooms.Add(s));
            context.SaveChanges();
            /* -----------end of table creation------------ */

            /* -----------start of table creation------------*/
            var seats = new List<Seat>
            {
            new Seat{RowX=1,RowY=1,SeatNumber=1,RoomID=1}
            };

            seats.ForEach(s => context.Seats.Add(s));
            context.SaveChanges();
             /*-----------end of table creation------------ */

            /* -----------start of table creation------------ */
            var shows = new List<Show>
            {
            new Show{BeginTime=DateTime.Parse("2018-03-06 16:15:00.000"),EndTime=DateTime.Parse("2018-04-06 18:30:00.000"),MovieID=9,RoomID=2,ChildDiscount=true,StudentDiscount=true,SeniorDiscount=true},
            new Show{BeginTime=DateTime.Parse("2018-03-06 19:00:00.000"),EndTime=DateTime.Parse("2018-04-06 21:00:00.000"),MovieID=1,RoomID=1,ChildDiscount=false,StudentDiscount=true,SeniorDiscount=true},
            new Show{BeginTime=DateTime.Parse("2018-03-06 19:00:00.000"),EndTime=DateTime.Parse("2018-04-06 21:00:00.000"),MovieID=2,RoomID=2,ChildDiscount=false,StudentDiscount=true,SeniorDiscount=true},
            new Show{BeginTime=DateTime.Parse("2018-03-06 19:00:00.000"),EndTime=DateTime.Parse("2018-04-06 21:00:00.000"),MovieID=3,RoomID=3,ChildDiscount=false,StudentDiscount=true,SeniorDiscount=true},
            new Show{BeginTime=DateTime.Parse("2018-03-06 19:00:00.000"),EndTime=DateTime.Parse("2018-04-06 21:00:00.000"),MovieID=4,RoomID=4,ChildDiscount=false,StudentDiscount=true,SeniorDiscount=true},
            new Show{BeginTime=DateTime.Parse("2018-03-06 19:00:00.000"),EndTime=DateTime.Parse("2018-04-06 21:00:00.000"),MovieID=5,RoomID=5,ChildDiscount=false,StudentDiscount=true,SeniorDiscount=true},
            new Show{BeginTime=DateTime.Parse("2018-03-06 19:00:00.000"),EndTime=DateTime.Parse("2018-04-06 21:00:00.000"),MovieID=6,RoomID=6,ChildDiscount=false,StudentDiscount=true,SeniorDiscount=true},
            new Show{BeginTime=DateTime.Parse("2018-03-06 21:15:00.000"),EndTime=DateTime.Parse("2018-04-06 23:15:00.000"),MovieID=7,RoomID=1,ChildDiscount=false,StudentDiscount=true,SeniorDiscount=true},
            new Show{BeginTime=DateTime.Parse("2018-03-06 21:15:00.000"),EndTime=DateTime.Parse("2018-04-06 22:30:00.000"),MovieID=8,RoomID=2,ChildDiscount=false,StudentDiscount=true,SeniorDiscount=true}
            };

            shows.ForEach(s => context.Shows.Add(s));
            context.SaveChanges();
            /* -----------end of table creation------------ */

            /* -----------start of table creation------------*/
            var tickets = new List<Ticket>
            {
            new Ticket{Price=12.00M,TicketType="Normaal",IsPaid=true,SeatID=1,ShowID=1}
            };

            tickets.ForEach(s => context.Tickets.Add(s));
            context.SaveChanges();
            /*-----------end of table creation------------ */

        }
    }
}
