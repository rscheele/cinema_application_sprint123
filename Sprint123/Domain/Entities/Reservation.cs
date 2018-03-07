﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Reservation
    {
        public Reservation()
        {

        }
        
        [Key]
        public int ReservationID { get; set; }
        public Movie BookedMovie { get; set; }
        public List<Ticket> Ticket { get; set; }
        public string Time { get; set; }

    }
}
