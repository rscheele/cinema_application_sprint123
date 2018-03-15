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
            new Location{Name="Cinema Breda",City="Breda", Rooms=6,TicketPrice=10.00M},
            new Location{Name="Cinema Eindhoven",City="Eindhoven", Rooms=6,TicketPrice=10.00M},
            new Location{Name="Cinema Rotterdam",City="Rotterdam", Rooms=6,TicketPrice=10.00M}
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
            new Movie{Name="De grote blije kinderfilm",Language="Nederlands",LanguageSub="Nederlands",Age=4,MovieType=2,Length=150,Is3D=false,LocationID=1},

            new Movie{Name="Darkest Hour",Language="Engels",LanguageSub="Nederlands", Age=16,MovieType=2,Length=120,Is3D=false,LocationID=2},
            new Movie{Name="Red Sparrow",Language="Engels",LanguageSub="Nederlands",Age=16,MovieType=2,Length=140,Is3D=false,LocationID=2},
            new Movie{Name="Death Wish",Language="Engels",LanguageSub="Nederlands",Age=16,MovieType=2,Length=108,Is3D=true,LocationID=2},
            new Movie{Name="Diep in de Zee",Language="Nederlands",LanguageSub="Nederlands",Age=6,MovieType=2,Length=91,Is3D=false,LocationID=2},
            new Movie{Name="Black Panther",Language="Engels",LanguageSub="Nederlands",Age=12,MovieType=2,Length=134,Is3D=true,LocationID=2},
            new Movie{Name="Bankier van het Verzet",Language="Nederlands",LanguageSub="Nederlands",Age=12,MovieType=2,Length=123,Is3D=false,LocationID=2},
            new Movie{Name="The Shape of Water",Language="Engels",LanguageSub="Nederlands",Age=16,MovieType=2,Length=123,Is3D=false,LocationID=2},
            new Movie{Name="Three BillBoards Outside Ebbing, Missouri",Language="Engels",LanguageSub="Nederlands",Age=16,MovieType=2,Length=115,Is3D=false,LocationID=2},
            new Movie{Name="De grote blije kinderfilm",Language="Nederlands",LanguageSub="Nederlands",Age=4,MovieType=2,Length=150,Is3D=false,LocationID=2},

            new Movie{Name="Darkest Hour",Language="Engels",LanguageSub="Nederlands", Age=16,MovieType=2,Length=120,Is3D=false,LocationID=3},
            new Movie{Name="Red Sparrow",Language="Engels",LanguageSub="Nederlands",Age=16,MovieType=2,Length=140,Is3D=false,LocationID=3},
            new Movie{Name="Death Wish",Language="Engels",LanguageSub="Nederlands",Age=16,MovieType=2,Length=108,Is3D=true,LocationID=3},
            new Movie{Name="Diep in de Zee",Language="Nederlands",LanguageSub="Nederlands",Age=6,MovieType=2,Length=91,Is3D=false,LocationID=3},
            new Movie{Name="Black Panther",Language="Engels",LanguageSub="Nederlands",Age=12,MovieType=2,Length=134,Is3D=true,LocationID=3},
            new Movie{Name="Bankier van het Verzet",Language="Nederlands",LanguageSub="Nederlands",Age=12,MovieType=2,Length=123,Is3D=false,LocationID=3},
            new Movie{Name="The Shape of Water",Language="Engels",LanguageSub="Nederlands",Age=16,MovieType=2,Length=123,Is3D=false,LocationID=3},
            new Movie{Name="Three BillBoards Outside Ebbing, Missouri",Language="Engels",LanguageSub="Nederlands",Age=16,MovieType=2,Length=115,Is3D=false,LocationID=3},
            new Movie{Name="De grote blije kinderfilm",Language="Nederlands",LanguageSub="Nederlands",Age=4,MovieType=2,Length=150,Is3D=false,LocationID=3}

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
            new Seat{RowX=0,SeatNumber=1,RoomID=1,RowY=0},
            new Seat{RowX=0,SeatNumber=1,RoomID=1,RowY=0},
            new Seat{RowX=0,SeatNumber=0,RoomID=1,RowY=0},
            new Seat{RowX=7,SeatNumber=68,RoomID=1,RowY=4},
            new Seat{RowX=8,SeatNumber=69,RoomID=1,RowY=4},
            new Seat{RowX=9,SeatNumber=70,RoomID=1,RowY=4},
            new Seat{RowX=6,SeatNumber=67,RoomID=1,RowY=4},
            new Seat{RowX=5,SeatNumber=66,RoomID=1,RowY=4},
            new Seat{RowX=4,SeatNumber=65,RoomID=1,RowY=4},
            new Seat{RowX=10,SeatNumber=71,RoomID=1,RowY=4},
            new Seat{RowX=3,SeatNumber=64,RoomID=1,RowY=4},
            new Seat{RowX=11,SeatNumber=72,RoomID=1,RowY=4},
            new Seat{RowX=2,SeatNumber=63,RoomID=1,RowY=4},
            new Seat{RowX=12,SeatNumber=73,RoomID=1,RowY=4},
            new Seat{RowX=1,SeatNumber=62,RoomID=1,RowY=4},
            new Seat{RowX=13,SeatNumber=74,RoomID=1,RowY=4},
            new Seat{RowX=7,SeatNumber=83,RoomID=1,RowY=5},
            new Seat{RowX=8,SeatNumber=84,RoomID=1,RowY=5},
            new Seat{RowX=9,SeatNumber=85,RoomID=1,RowY=5},
            new Seat{RowX=10,SeatNumber=86,RoomID=1,RowY=5},
            new Seat{RowX=11,SeatNumber=87,RoomID=1,RowY=5},
            new Seat{RowX=12,SeatNumber=88,RoomID=1,RowY=5},
            new Seat{RowX=5,SeatNumber=81,RoomID=1,RowY=5},
            new Seat{RowX=6,SeatNumber=82,RoomID=1,RowY=5},
            new Seat{RowX=3,SeatNumber=79,RoomID=1,RowY=5},
            new Seat{RowX=4,SeatNumber=80,RoomID=1,RowY=5},
            new Seat{RowX=1,SeatNumber=77,RoomID=1,RowY=5},
            new Seat{RowX=2,SeatNumber=78,RoomID=1,RowY=5},
            new Seat{RowX=0,SeatNumber=61,RoomID=1,RowY=4},
            new Seat{RowX=7,SeatNumber=98,RoomID=1,RowY=6},
            new Seat{RowX=8,SeatNumber=99,RoomID=1,RowY=6},
            new Seat{RowX=9,SeatNumber=100,RoomID=1,RowY=6},
            new Seat{RowX=10,SeatNumber=101,RoomID=1,RowY=6},
            new Seat{RowX=11,SeatNumber=102,RoomID=1,RowY=6},
            new Seat{RowX=4,SeatNumber=95,RoomID=1,RowY=6},
            new Seat{RowX=5,SeatNumber=96,RoomID=1,RowY=6},
            new Seat{RowX=6,SeatNumber=97,RoomID=1,RowY=6},
            new Seat{RowX=12,SeatNumber=103,RoomID=1,RowY=6},
            new Seat{RowX=13,SeatNumber=104,RoomID=1,RowY=6},
            new Seat{RowX=14,SeatNumber=105,RoomID=1,RowY=6},

            new Seat{RowX=7,SeatNumber=68,RoomID=2,RowY=4},
            new Seat{RowX=8,SeatNumber=69,RoomID=2,RowY=4},

            

            new Seat{RowX=6,SeatNumber=67,RoomID=2,RowY=4},

            

            new Seat{RowX=5,SeatNumber=66,RoomID=2,RowY=4},

           

            new Seat{RowX=7,SeatNumber=68,RoomID=3,RowY=4},
            new Seat{RowX=8,SeatNumber=69,RoomID=3,RowY=4},
            new Seat{RowX=9,SeatNumber=70,RoomID=3,RowY=4},

            new Seat{RowX=13,SeatNumber=89,RoomID=1,RowY=5},
            new Seat{RowX=14,SeatNumber=90,RoomID=1,RowY=5},

            new Seat{RowX=5,SeatNumber=36,RoomID=4,RowY=5},

            new Seat{RowX=14,SeatNumber=75,RoomID=1,RowY=3},

            new Seat{RowX=4,SeatNumber=35,RoomID=4,RowY=3},

            new Seat{RowX=0,SeatNumber=76,RoomID=1,RowY=5},

            new Seat{RowX=6,SeatNumber=37,RoomID=4,RowY=3},
            new Seat{RowX=7,SeatNumber=38,RoomID=4,RowY=3},
            new Seat{RowX=3,SeatNumber=44,RoomID=4,RowY=4},
            new Seat{RowX=4,SeatNumber=45,RoomID=4,RowY=4},
            new Seat{RowX=5,SeatNumber=46,RoomID=4,RowY=4},
            new Seat{RowX=6,SeatNumber=47,RoomID=4,RowY=4},
            new Seat{RowX=7,SeatNumber=48,RoomID=4,RowY=4},
            new Seat{RowX=8,SeatNumber=49,RoomID=4,RowY=4},
            new Seat{RowX=9,SeatNumber=50,RoomID=4,RowY=4},

            new Seat{RowX=3,SeatNumber=94,RoomID=1,RowY=6},

            new Seat{RowX=5,SeatNumber=36,RoomID=4,RowY=3},

            new Seat{RowX=7,SeatNumber=68,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,RoomID=3,RowY=4},
            new Seat{RowX=6,SeatNumber=67,RoomID=3,RowY=4},
            new Seat{RowX=7,SeatNumber=68,RoomID=3,RowY=4},
            new Seat{RowX=8,SeatNumber=69,RoomID=3,RowY=4},
            new Seat{RowX=9,SeatNumber=70,RoomID=3,RowY=4},

            new Seat{RowX=1,SeatNumber=32,RoomID=4,RowY=3},
            new Seat{RowX=2,SeatNumber=33,RoomID=4,RowY=3},
            new Seat{RowX=3,SeatNumber=34,RoomID=4,RowY=3},
            new Seat{RowX=0,SeatNumber=41,RoomID=4,RowY=4},
            new Seat{RowX=1,SeatNumber=42,RoomID=4,RowY=4},
            new Seat{RowX=2,SeatNumber=43,RoomID=4,RowY=4},
            new Seat{RowX=5,SeatNumber=56,RoomID=4,RowY=5},
            new Seat{RowX=6,SeatNumber=57,RoomID=4,RowY=5},
            new Seat{RowX=7,SeatNumber=58,RoomID=4,RowY=5},

            new Seat{RowX=5,SeatNumber=66,RoomID=3,RowY=4},
            new Seat{RowX=9,SeatNumber=70,RoomID=2,RowY=4},

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
            new Ticket{Price=9.50M,TicketType="Normaal",IsPaid=true,ShowID=1,SeatID=5,ReservationID=20180681426909,Popcorn=false},
            new Ticket{Price=13.00M,TicketType="Kind",IsPaid=true,ShowID=1,SeatID=6,ReservationID=20180681426909,Popcorn=true},
            new Ticket{Price=13.00M,TicketType="Kind",IsPaid=true,ShowID=1,SeatID=7,ReservationID=20180681426909,Popcorn=true},
            new Ticket{Price=12.00M,TicketType="Normaal",IsPaid=true,ShowID=3,SeatID=11,ReservationID=20180681426908,Popcorn=false},
            new Ticket{Price=12.00M,TicketType="Normaal",IsPaid=true,ShowID=3,SeatID=12,ReservationID=20180681426908,Popcorn=false},
            new Ticket{Price=15.00M,TicketType="Kind",IsPaid=true,ShowID=3,SeatID=13,ReservationID=20180681426908,Popcorn=true},
            new Ticket{Price=10.00M,TicketType="Kind",IsPaid=true,ShowID=3,SeatID=14,ReservationID=20180681426908,Popcorn=false},
            new Ticket{Price=10.50M,TicketType="Senior",IsPaid=true,ShowID=3,SeatID=15,ReservationID=20180681426908,Popcorn=false}
            };

            tickets.ForEach(s => context.Tickets.Add(s));
            context.SaveChanges();
            /*-----------end of table creation------------ */

        }
    }
}
