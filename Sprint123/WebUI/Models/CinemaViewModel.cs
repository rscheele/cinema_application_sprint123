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
        public List<SeatCoord> SeatCoordList;
        public Show SelectedShow;

    }

}