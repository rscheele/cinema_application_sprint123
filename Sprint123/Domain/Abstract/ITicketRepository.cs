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
        IEnumerable<Ticket> GetTickets(long reservationID);
        IEnumerable<Ticket> GetMovieSecretTickets();
        void SaveTickets(List<Ticket> tickets);
    }
}
