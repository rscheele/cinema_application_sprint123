using System;
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
        public int TicketID { get; set; }
        public decimal Price { get; set; }
        public int TicketType { get; set; }//could be enum
        public decimal Discount { get; set; }
        public bool IsPaid { get; set; }

        public int? SeatID { get; set; }
        public int? ShowID { get; set; }

        [ForeignKey("SeatID")]
        public virtual Seat seat { get; set; }
        [ForeignKey("ShowID")]
        public virtual Show show { get; set; }


    }
}
