using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class TicketRepository : ITicketRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Ticket> GetMovieSecretTickets()
        {
            return context.Tickets;
        }

        public IEnumerable<Ticket> GetTickets(long reservationID)
        {
            List<Ticket> list = context.Tickets.Where(x => x.ReservationID == reservationID).ToList();
            if (list != null)
            {
                return list;
            } else
            {
                return null;
            }
            
        }
        public void SaveTickets(List<Ticket> tickets)
        {
            foreach (var item in tickets)
            {
                context.Tickets.Add(item);
            }
            context.SaveChanges();
        }
    }
        
}
