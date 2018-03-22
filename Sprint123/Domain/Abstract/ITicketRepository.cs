using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetTickets(long ReservationID);
        IEnumerable<Ticket> GetShowTickets(int ShowID);
        IEnumerable<Ticket> GetMovieSecretTickets();
        void SaveTickets(List<Ticket> Tickets);
    }
}
