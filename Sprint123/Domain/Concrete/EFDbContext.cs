using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFDbContext : DbContext {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Location> Locations { get; set; }

    }
    
}
