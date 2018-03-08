using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class ReservationRepository : IReservationRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Reservation> GetReservations()
        {
            return context.Reservations;
        }
    }
}
