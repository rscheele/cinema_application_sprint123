using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;
using Domain.Concrete;

namespace WebUI.Models
{
    public class AutomaticSeatSelection
    {

        public enum OccupyType
        {
            Unoccupied,
            Occupied,
            Selected
        }

        public List<List<OccupyType>> VisualizeSeats(Room room, List<Seat> occupiedSeats, List<SeatCoord> seatCoords)
        {
            var retList = new List<List<OccupyType>>();

            // gets all the seats where the roomid is equal to room roomid and where there are tickets off with the ticketid
            // List<Seat> occupiedSeats = context.Seats.ToList().Where(s => s.RoomID == room.RoomID && context.Tickets.ToList().Find(t => t.SeatID == s.SeatID) != null).ToList();
            var layout = room.Layout;

            for (int i = 0; i < layout.FrontY; i++)
            {
                var rowList = new List<OccupyType>();
                for (int j = 0; j < layout.FrontX; j++)
                {
                    OccupyType type;
                    IsOccupiedSeat(occupiedSeats, seatCoords, i, j, false, out type);
                    rowList.Add(type);
                }
                retList.Add(rowList);
            }

            // null to seperate front and back
            retList.Add(null);
            for (int i = 0; i < layout.BackY; i++)
            {
                var rowList = new List<OccupyType>();
                for (int j = 0; j < layout.BackX; j++)
                {
                    OccupyType type;
                    IsOccupiedSeat(occupiedSeats, seatCoords, i + layout.BackY, j, false, out type);
                    rowList.Add(type);
                }
                retList.Add(rowList);
            }

            return retList;
        }

        public bool IsOccupiedSeat(List<Seat> seats, List<SeatCoord> extraSeatCoords, int y, int x, bool debug_, out OccupyType type)
        {
            if (debug_)
            {
                //debug.Add(String.Format("   Checking: Y = {0}, X = {1}", y, x));
            }
            foreach (Seat s in seats)
            {
                if (s.RowX == x && s.RowY == y)
                {
                    type = OccupyType.Occupied;
                    return true;
                }
            }
            if (extraSeatCoords != null)
            {
                foreach (SeatCoord sc in extraSeatCoords)
                {
                    if (sc.X == x && sc.Y == y)
                    {
                        type = OccupyType.Selected;
                        return true;
                    }
                }
            }

            type = OccupyType.Unoccupied;
            return false;
        }

    }
}