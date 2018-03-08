using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Abstract;
using Domain.Entities;

namespace WebUI.Models
{
    public class SeatCoord
    {
        public int Y;
        public int X;

        public SeatCoord(int y, int x)
        {
            this.Y = y;
            this.X = x;
        }
    }
}