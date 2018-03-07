using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Boolean
    {

        public Boolean()
        {

        }
        [Key]
        public int MovieID { get; set; }

        public string Name { get; set; }
        public string Language { get; set; }
        public string LanguageSub { get; set; }
        public int Age { get; set; }
        public int MovieType { get; set; }
        public int Length { get; set; }
        public bool Is3D { get; set; }

        public int LocationID { get; set; }

        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }
    }
}
