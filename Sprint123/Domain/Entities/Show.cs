using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Show
    {

        public Show()
        {

        }

        [Key]
        public int ShowID { get; set; }

        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }

        public int? MovieID { get; set; }

        [ForeignKey("MovieID")]
        public virtual Boolean Movie { get; set; }

        public int? RoomID { get; set; }

        [ForeignKey("RoomID")]
        public virtual Room Room { get; set; }

        public bool ChildDiscount { get; set; }
        public bool StudentDiscount { get; set; }
        public bool SeniorDiscount { get; set; }

        public bool threeD { get; set; }

        public bool Popcorn { get; set; }

        public bool longFilm { get; set; }

    }
}
