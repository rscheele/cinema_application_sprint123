using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TempTicket
    {
        public TempTicket()
        {

        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public long ReservationID { get; set; }
        public int? SeatID { get; set; }
        public int? ShowID { get; set; }
        public DateTime TimeAdded { get; set; }
    }
}
