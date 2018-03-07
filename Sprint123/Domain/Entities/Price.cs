using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Price
    {
        public Price()
        {

        }

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }

    }
}
