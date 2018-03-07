﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ticket
    {
        public Ticket() {

        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TicketID { get; set; }
        public decimal Price { get; set; }
        public string TicketType { get; set; }//could be enum
        public bool IsPaid { get; set; }
        public int? SeatID { get; set; }
        public int? ShowID { get; set; }

        [ForeignKey("SeatID")]
        public virtual Seat Seat { get; set; }
        [ForeignKey("ShowID")]
        public virtual Show Show { get; set; }
        public bool Popcorn { get; set; }


    }
}
