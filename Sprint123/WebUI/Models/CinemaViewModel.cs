using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Abstract;
using Domain.Entities;
using Domain.Concrete;

namespace WebUI.Models
{
    public class CinemaViewModel
    {
        public List<List<AutomaticSeatSelection.OccupyType>> SeatSelectionGUI;
        public List<SeatCoord> SeatCoordList;
        public Show SelectedShow;

        public CinemaViewModel()
        {
            SeatCoordList = new List<SeatCoord>();
        }

        public int GetSeatsRow()
        {
            return SeatCoordList[0].Y;
        }

        public String getSeatNumbersString()
        {
            String str = "";

            foreach (SeatCoord sc in SeatCoordList)
            {
                str += sc.GetSeatNumber(SelectedShow.Room.Layout);
                str += ", ";
            }

            str = str.Remove(str.Length - 2);

            return str;
        }

        public int TotalFreeSeats()
        {
            var rl = SelectedShow.Room.Layout;
            int roomSeats = (rl.BackX * rl.BackY) + (rl.FrontX * rl.FrontY);
            int seatsUsed = (new EFDbContext()).Tickets.Where(t => t.ShowID == SelectedShow.ShowID)
                    .GroupBy(x => x.SeatID).Select(y => y.FirstOrDefault()).ToList().Count();
            return roomSeats - seatsUsed;
        }

        public int TotalSeats()
        {
            var rl = SelectedShow.Room.Layout;
            int roomSeats = (rl.BackX * rl.BackY) + (rl.FrontX * rl.FrontY);

            return roomSeats;
        }

        public List<Seat> GetSeatsList()
        {
            var list = new List<Seat>();

            foreach (SeatCoord sc in SeatCoordList)
            {
                list.Add(new Seat()
                {
                    //Room = SelectedShowing.Room,
                    RoomID = SelectedShow.Room.RoomID,
                    RowX = sc.X,
                    RowY = sc.Y,
                    SeatNumber = sc.GetSeatNumber(SelectedShow.Room.Layout),
                });
            }

            return list;
        }

        public List<Ticket> GetTicketsList(List<Seat> seatsList)
        {
            var list = new List<Ticket>();
            //var seatsList = GetSeatsList();

            // To do check per location 3D movies discount/extra cost?
            // 3D movies have extra cost so return minus
            Decimal discount = SelectedFilm.Is3D ? -2.5m : 0.0m;

            //TODO APPLY ADDITIONAL PER LOCATION DISCOUNTS


            foreach (SeatCoord sc in SeatCoordList)
            {
                if (SelectedEmployee != null)
                {

                    var ticket = new Ticket()
                    {
                        Discount = discount,
                        //Customer = customer,
                        CustomerID = customer.CustomerID,
                        Price = 0.0m,
                        //Seat = seatsList.Find(s => s.seatNo == sc.GetSeatNumber(SelectedShowing.Room.Layout)),
                        SeatID = seatsList.Where(s => s.RowX == sc.X && s.RowY == s.RowY).Last().SeatID,
                        SecretKey = secretKey,
                        TicketType = (int)TicketType.InvalidTicket,
                        //Showing = SelectedShowing,
                        ShowingID = SelectedShowing.ShowingID,
                        //Check if this has been reached by MasterCard or iDeal -> isPaid = true, else false
                        IsPaid = this.isPaid,
                        Employee = SelectedEmployee.Name

                    };
                    list.Add(ticket);
                }
                else
                {
                    var ticket = new Ticket()
                    {
                        Discount = discount,
                        //Customer = customer,
                        CustomerID = customer.CustomerID,
                        Price = 0.0m,
                        //Seat = seatsList.Find(s => s.seatNo == sc.GetSeatNumber(SelectedShowing.Room.Layout)),
                        SeatID = seatsList.Where(s => s.RowX == sc.X && s.RowY == s.RowY).Last().SeatID,
                        SecretKey = secretKey,
                        TicketType = (int)TicketType.InvalidTicket,
                        //Showing = SelectedShowing,
                        ShowingID = SelectedShowing.ShowingID,
                        //Check if this has been reached by MasterCard or iDeal -> isPaid = true, else false
                        IsPaid = this.isPaid

                    };
                    list.Add(ticket);
                }

                int normal = 0; int senior = 0; int student = 0; int child = 0; int popcorn = 0; int ladies = 0; int vip = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    if (NormalTicketOrder.Quantity > normal)
                    {
                        list[i].TicketType = (int)TicketType.NormalTicket;
                        list[i].Price = NormalTicketOrder.GetPrice();
                        // NO DISCOUNT
                        normal++;
                    }
                    else if (SeniorTicketOrder.Quantity > senior)
                    {
                        list[i].TicketType = (int)TicketType.SeniorTicket;
                        list[i].Price = SeniorTicketOrder.GetPrice();
                        list[i].Discount += SeniorTicketOrder.GetDiscount();
                        senior++;
                    }
                    else if (ChildTicketOrder.Quantity > child)
                    {
                        list[i].TicketType = (int)TicketType.ChildTicket;
                        list[i].Price = ChildTicketOrder.GetPrice();
                        list[i].Discount += ChildTicketOrder.GetDiscount();
                        child++;
                    }
                    else if (StudentTicketOrder.Quantity > student)
                    {
                        list[i].TicketType = (int)TicketType.StudentTicket;
                        list[i].Price = StudentTicketOrder.GetPrice();
                        list[i].Discount += StudentTicketOrder.GetDiscount();
                        student++;
                    }
                    else if (PopcornTicketOrder.Quantity > popcorn)
                    {
                        list[i].TicketType = (int)TicketType.PopcornTicket;
                        list[i].Price = PopcornTicketOrder.GetPrice();
                        list[i].Discount += PopcornTicketOrder.GetDiscount();
                        popcorn++;
                    }
                    else if (LadiesTicketOrder.Quantity > ladies)
                    {
                        list[i].TicketType = (int)TicketType.LadiesTicket;
                        list[i].Price = LadiesTicketOrder.GetPrice();
                        list[i].Discount += LadiesTicketOrder.GetDiscount();
                    }
                    else if (VIPTicketOrder.Quantity > vip)
                    {
                        list[i].TicketType = (int)TicketType.VIPTicket;
                        list[i].Price = VIPTicketOrder.GetPrice();
                        list[i].Discount += VIPTicketOrder.GetDiscount();
                    }

                }
            }

            return list;
        }

        //TODO ADD GetAllTicketsQuantity Method.

        //TODO ADD GetTicketLists Method.




    }

}