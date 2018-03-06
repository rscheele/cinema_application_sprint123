﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RoomLayout
    {
        public RoomLayout() {

        }

        [Key]
        public int? LayoutID { get; set; }

        public int FrontX {get; set;}
        public int FrontY {get; set;}
        public int BackX {get; set;}
        public int BackY {get; set;}
    }
}
