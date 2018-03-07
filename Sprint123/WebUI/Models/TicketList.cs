using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace WebUI.Models
{
    public class TicketList
    {
        public int Id { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}