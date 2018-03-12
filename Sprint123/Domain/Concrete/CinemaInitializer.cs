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
            var movies = new List<Movie>
            {
            new Movie{Name="Darkest Hour",Language="Engels",LanguageSub="Nederlands", Age=16,MovieType=2,Length=120,Is3D=false,LocationID=1},
            new Movie{Name="Red Sparrow",Language="Engels",LanguageSub="Nederlands",Age=16,MovieType=2,Length=140,Is3D=false,LocationID=1},
            new Movie{Name="Death Wish",Language="Engels",LanguageSub="Nederlands",Age=16,MovieType=2,Length=108,Is3D=true,LocationID=1},
            new Movie{Name="Diep in de Zee",Language="Nederlands",LanguageSub="Nederlands",Age=6,MovieType=2,Length=91,Is3D=false,LocationID=1},
            new Movie{Name="Black Panther",Language="Engels",LanguageSub="Nederlands",Age=12,MovieType=2,Length=134,Is3D=true,LocationID=1},
            new Movie{Name="Bankier van het Verzet",Language="Nederlands",LanguageSub="Nederlands",Age=12,MovieType=2,Length=123,Is3D=false,LocationID=1},
            new Movie{Name="The Shape of Water",Language="Engels",LanguageSub="Nederlands",Age=16,MovieType=2,Length=123,Is3D=false,LocationID=1},
            new Movie{Name="Three BillBoards Outside Ebbing, Missouri",Language="Engels",LanguageSub="Nederlands",Age=16,MovieType=2,Length=115,Is3D=false,LocationID=1},
            new Movie{Name="De grote blije kinderfilm",Language="Nederlands",LanguageSub="Nederlands",Age=4,MovieType=2,Length=150,Is3D=false,LocationID=1}
            };

            movies.ForEach(s => context.Movies.Add(s));
            context.SaveChanges();
            /* -----------end of table creation------------ */

            /* -----------start of table creation------------ */
            var roomlayouts = new List<RoomLayout>
            {
            new RoomLayout{FrontX=20, FrontY=12, BackX=0, BackY=0},
            new RoomLayout{FrontX=11, FrontY=14, BackX=2, BackY=10},
            new RoomLayout{FrontX=10, FrontY=2, BackX=15, BackY=2}
            };

            roomlayouts.ForEach(s => context.RoomLayouts.Add(s));
            context.SaveChanges();
            /* -----------end of table creation------------ */

            /* -----------start of table creation------------ */
            var rooms = new List<Room>
            {
            new Room{RoomNumber=1,LayoutID=1,LocationID=1},
            new Room{RoomNumber=2,LayoutID=1,LocationID=1},
            new Room{RoomNumber=3,LayoutID=1,LocationID=1},
            new Room{RoomNumber=4,LayoutID=2,LocationID=1},
            new Room{RoomNumber=5,LayoutID=3,LocationID=1},
            new Room{RoomNumber=6,LayoutID=3,LocationID=1}
            };

            rooms.ForEach(s => context.Rooms.Add(s));
            context.SaveChanges();
            /* -----------end of table creation------------ */

            /* -----------start of table creation------------*/
            var seats = new List<Seat>
            {
            new Seat{RowX=0,SeatNumber=1,SeatID=2,RoomID=1,RowY=0},
            new Seat{RowX=0,SeatNumber=1,SeatID=3,RoomID=1,RowY=0},
            new Seat{RowX=0,SeatNumber=0,SeatID=4,RoomID=1,RowY=0},
            new Seat{RowX=7,SeatNumber=68,SeatID=5,RoomID=1,RowY=4},
            new Seat{RowX=8,SeatNumber=69,SeatID=6,RoomID=1,RowY=4},
            new Seat{RowX=9,SeatNumber=70,SeatID=7,RoomID=1,RowY=4},
            new Seat{RowX=6,SeatNumber=67,SeatID=8,RoomID=1,RowY=4},
            new Seat{RowX=7,SeatNumber=68,SeatID=9,RoomID=2,RowY=4},
            new Seat{RowX=8,SeatNumber=69,SeatID=10,RoomID=2,RowY=4},
            new Seat{RowX=5,SeatNumber=66,SeatID=11,RoomID=1,RowY=4},
            new Seat{RowX=4,SeatNumber=65,SeatID=12,RoomID=1,RowY=4},
            new Seat{RowX=10,SeatNumber=71,SeatID=13,RoomID=1,RowY=4},
            new Seat{RowX=6,SeatNumber=67,SeatID=14,RoomID=2,RowY=4},
            new Seat{RowX=3,SeatNumber=64,SeatID=15,RoomID=1,RowY=4},
            new Seat{RowX=11,SeatNumber=72,SeatID=16,RoomID=1,RowY=4},
            new Seat{RowX=2,SeatNumber=63,SeatID=17,RoomID=1,RowY=4},
            new Seat{RowX=12,SeatNumber=73,SeatID=18,RoomID=1,RowY=4},
            new Seat{RowX=1,SeatNumber=62,SeatID=19,RoomID=1,RowY=4},
            new Seat{RowX=13,SeatNumber=74,SeatID=20,RoomID=1,RowY=4},
            new Seat{RowX=5,SeatNumber=66,SeatID=21,RoomID=2,RowY=4},
            new Seat{RowX=7,SeatNumber=83,SeatID=22,RoomID=1,RowY=5},
            new Seat{RowX=8,SeatNumber=84,SeatID=23,RoomID=1,RowY=5},
            new Seat{RowX=9,SeatNumber=85,SeatID=24,RoomID=1,RowY=5},
            new Seat{RowX=10,SeatNumber=86,SeatID=25,RoomID=1,RowY=5},
            new Seat{RowX=11,SeatNumber=87,SeatID=26,RoomID=1,RowY=5},
            new Seat{RowX=12,SeatNumber=88,SeatID=27,RoomID=1,RowY=5},
            new Seat{RowX=5,SeatNumber=81,SeatID=28,RoomID=1,RowY=5},
            new Seat{RowX=6,SeatNumber=82,SeatID=29,RoomID=1,RowY=5},
            new Seat{RowX=3,SeatNumber=79,SeatID=30,RoomID=1,RowY=5},
            new Seat{RowX=4,SeatNumber=80,SeatID=31,RoomID=1,RowY=5},
            new Seat{RowX=1,SeatNumber=77,SeatID=32,RoomID=1,RowY=5},
            new Seat{RowX=2,SeatNumber=78,SeatID=33,RoomID=1,RowY=5},
            new Seat{RowX=0,SeatNumber=61,SeatID=34,RoomID=1,RowY=4},
            new Seat{RowX=7,SeatNumber=98,SeatID=35,RoomID=1,RowY=6},
            new Seat{RowX=8,SeatNumber=99,SeatID=36,RoomID=1,RowY=6},
            new Seat{RowX=9,SeatNumber=100,SeatID=37,RoomID=1,RowY=6},
            new Seat{RowX=10,SeatNumber=101,SeatID=38,RoomID=1,RowY=6},
            new Seat{RowX=11,SeatNumber=102,SeatID=39,RoomID=1,RowY=6},
            new Seat{RowX=4,SeatNumber=95,SeatID=40,RoomID=1,RowY=6},
            new Seat{RowX=5,SeatNumber=96,SeatID=41,RoomID=1,RowY=6},
            new Seat{RowX=6,SeatNumber=97,SeatID=42,RoomID=1,RowY=6},
            new Seat{RowX=12,SeatNumber=103,SeatID=43,RoomID=1,RowY=6},
            new Seat{RowX=13,SeatNumber=104,SeatID=44,RoomID=1,RowY=6},
            new Seat{RowX=14,SeatNumber=105,SeatID=45,RoomID=1,RowY=6},
            new Seat{RowX=7,SeatNumber=68,SeatID=46,RoomID=3,RowY=4},
            new Seat{RowX=8,SeatNumber=69,SeatID=47,RoomID=3,RowY=4},
            new Seat{RowX=9,SeatNumber=70,SeatID=48,RoomID=3,RowY=4},
            new Seat{RowX=13,SeatNumber=89,SeatID=49,RoomID=1,RowY=5},
            new Seat{RowX=14,SeatNumber=90,SeatID=50,RoomID=1,RowY=5},
            new Seat{RowX=5,SeatNumber=36,SeatID=51,RoomID=4,RowY=5},
            new Seat{RowX=14,SeatNumber=75,SeatID=52,RoomID=1,RowY=3},
            new Seat{RowX=4,SeatNumber=35,SeatID=53,RoomID=4,RowY=3},
            new Seat{RowX=0,SeatNumber=76,SeatID=54,RoomID=1,RowY=5},
            new Seat{RowX=7,SeatNumber=53,SeatID=55,RoomID=7,RowY=3},
            new Seat{RowX=8,SeatNumber=54,SeatID=56,RoomID=7,RowY=3},
            new Seat{RowX=9,SeatNumber=55,SeatID=57,RoomID=7,RowY=3},
            new Seat{RowX=10,SeatNumber=56,SeatID=58,RoomID=7,RowY=3},
            new Seat{RowX=11,SeatNumber=57,SeatID=59,RoomID=7,RowY=3},
            new Seat{RowX=12,SeatNumber=58,SeatID=60,RoomID=7,RowY=3},
            new Seat{RowX=6,SeatNumber=37,SeatID=61,RoomID=4,RowY=3},
            new Seat{RowX=7,SeatNumber=38,SeatID=62,RoomID=4,RowY=3},
            new Seat{RowX=3,SeatNumber=44,SeatID=63,RoomID=4,RowY=4},
            new Seat{RowX=4,SeatNumber=45,SeatID=64,RoomID=4,RowY=4},
            new Seat{RowX=5,SeatNumber=46,SeatID=65,RoomID=4,RowY=4},
            new Seat{RowX=6,SeatNumber=47,SeatID=66,RoomID=4,RowY=4},
            new Seat{RowX=7,SeatNumber=48,SeatID=67,RoomID=4,RowY=4},
            new Seat{RowX=8,SeatNumber=49,SeatID=68,RoomID=4,RowY=4},
            new Seat{RowX=9,SeatNumber=50,SeatID=69,RoomID=4,RowY=4},
            new Seat{RowX=3,SeatNumber=94,SeatID=70,RoomID=1,RowY=6},
            new Seat{RowX=5,SeatNumber=36,SeatID=71,RoomID=4,RowY=3},
            new Seat{RowX=7,SeatNumber=68,SeatID=72,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,SeatID=73,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,SeatID=74,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,SeatID=75,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,SeatID=76,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,SeatID=77,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,SeatID=78,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,SeatID=79,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,SeatID=80,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,SeatID=81,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,SeatID=82,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,SeatID=83,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,SeatID=84,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,SeatID=85,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,SeatID=86,RoomID=3,RowY=4},
            new Seat{RowX=7,SeatNumber=68,SeatID=87,RoomID=3,RowY=4},
            new Seat{RowX=8,SeatNumber=69,SeatID=88,RoomID=3,RowY=4},
            new Seat{RowX=9,SeatNumber=70,SeatID=89,RoomID=3,RowY=4},
            new Seat{RowX=1,SeatNumber=32,SeatID=90,RoomID=4,RowY=3},
            new Seat{RowX=2,SeatNumber=33,SeatID=91,RoomID=4,RowY=3},
            new Seat{RowX=3,SeatNumber=34,SeatID=92,RoomID=4,RowY=3},
            new Seat{RowX=0,SeatNumber=41,SeatID=93,RoomID=4,RowY=4},
            new Seat{RowX=1,SeatNumber=42,SeatID=94,RoomID=4,RowY=4},
            new Seat{RowX=2,SeatNumber=43,SeatID=95,RoomID=4,RowY=4},
            new Seat{RowX=5,SeatNumber=56,SeatID=96,RoomID=4,RowY=5},
            new Seat{RowX=6,SeatNumber=57,SeatID=97,RoomID=4,RowY=5},
            new Seat{RowX=7,SeatNumber=58,SeatID=98,RoomID=4,RowY=5},
            new Seat{RowX=5,SeatNumber=66,SeatID=99,RoomID=3,RowY=4},
            new Seat{RowX=9,SeatNumber=70,SeatID=100,RoomID=2,RowY=4},

            };

            seats.ForEach(s => context.Seats.Add(s));
            context.SaveChanges();
             /*-----------end of table creation------------ */

            /* -----------start of table creation------------ */
            var shows = new List<Show>
            {
            new Show{BeginTime=DateTime.Parse("2018-03-06 16:15:00.000"),EndTime=DateTime.Parse("2018-04-06 18:30:00.000"),MovieID=9,RoomID=2,NumberofTickets=11,ChildDiscount=true,StudentDiscount=true,SeniorDiscount=true},
            new Show{BeginTime=DateTime.Parse("2018-03-06 19:00:00.000"),EndTime=DateTime.Parse("2018-04-06 21:00:00.000"),MovieID=1,RoomID=1,NumberofTickets=10,ChildDiscount=false,StudentDiscount=true,SeniorDiscount=true},
            new Show{BeginTime=DateTime.Parse("2018-03-06 19:00:00.000"),EndTime=DateTime.Parse("2018-04-06 21:00:00.000"),MovieID=2,RoomID=2,NumberofTickets=3,ChildDiscount=false,StudentDiscount=true,SeniorDiscount=true},
            new Show{BeginTime=DateTime.Parse("2018-03-06 19:08:00.000"),EndTime=DateTime.Parse("2018-04-06 21:08:00.000"),MovieID=3,RoomID=3,NumberofTickets=1,ChildDiscount=false,StudentDiscount=true,SeniorDiscount=true},
            new Show{BeginTime=DateTime.Parse("2018-03-06 19:11:00.000"),EndTime=DateTime.Parse("2018-04-06 21:11:00.000"),MovieID=4,RoomID=4,NumberofTickets=3,ChildDiscount=false,StudentDiscount=true,SeniorDiscount=true},
            new Show{BeginTime=DateTime.Parse("2018-03-06 19:00:00.000"),EndTime=DateTime.Parse("2018-04-06 21:00:00.000"),MovieID=5,RoomID=5,NumberofTickets=6,ChildDiscount=false,StudentDiscount=true,SeniorDiscount=true},
            new Show{BeginTime=DateTime.Parse("2018-03-06 19:00:00.000"),EndTime=DateTime.Parse("2018-04-06 21:00:00.000"),MovieID=6,RoomID=6,NumberofTickets=2,ChildDiscount=false,StudentDiscount=true,SeniorDiscount=true},
            new Show{BeginTime=DateTime.Parse("2018-03-06 21:15:00.000"),EndTime=DateTime.Parse("2018-04-06 23:15:00.000"),MovieID=7,RoomID=1,NumberofTickets=10,ChildDiscount=false,StudentDiscount=true,SeniorDiscount=true},
            new Show{BeginTime=DateTime.Parse("2018-03-06 21:15:00.000"),EndTime=DateTime.Parse("2018-04-06 22:30:00.000"),MovieID=8,RoomID=2,NumberofTickets=15,ChildDiscount=false,StudentDiscount=true,SeniorDiscount=true}
            };

            shows.ForEach(s => context.Shows.Add(s));
            context.SaveChanges();
            /* -----------end of table creation------------ */

            /* -----------start of table creation------------*/
            var tickets = new List<Ticket>
            {
            // Reservation 20180681426908 20180681426909
            new Ticket{Price=9.50M,TicketType="Normaal",IsPaid=true,ShowID=1,ReservationID=20180681426909,Popcorn=false},
            new Ticket{Price=13.00M,TicketType="Child",IsPaid=true,ShowID=1,ReservationID=20180681426909,Popcorn=true},
            new Ticket{Price=13.00M,TicketType="Child",IsPaid=true,ShowID=1,ReservationID=20180681426909,Popcorn=true},
            new Ticket{Price=12.00M,TicketType="Normaal",IsPaid=true,ShowID=3,ReservationID=20180681426908,Popcorn=false},
            new Ticket{Price=12.00M,TicketType="Normaal",IsPaid=true,ShowID=3,ReservationID=20180681426908,Popcorn=false},
            new Ticket{Price=15.00M,TicketType="Child",IsPaid=true,ShowID=3,ReservationID=20180681426908,Popcorn=true},
            new Ticket{Price=10.00M,TicketType="Child",IsPaid=true,ShowID=3,ReservationID=20180681426908,Popcorn=false},
            new Ticket{Price=10.50M,TicketType="Senior",IsPaid=true,ShowID=3,ReservationID=20180681426908,Popcorn=false}
            };

            tickets.ForEach(s => context.Tickets.Add(s));
            context.SaveChanges();
            /*-----------end of table creation------------ */

        }
    }
}
