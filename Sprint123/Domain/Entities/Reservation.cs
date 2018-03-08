using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Reservation
    {
        public Reservation()
        {

        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int ReservationID { get; set; }
        public int ShowID { get; set; } //refence of show == bookedshow
        public List<Ticket> Ticket { get; set; }
        public DateTime Time { get; set; }


        [ForeignKey("ShowID")]// link reservation with show
        public virtual Show Show { get; set; }
    }
}

