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


        //TODO ADD GetAllTicketsQuantity Method.

        //TODO ADD GetTicketLists Method.




    }

}