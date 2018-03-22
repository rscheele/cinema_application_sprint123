using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface ITempTicketRepository
    {
        IEnumerable<TempTicket> GetTempTickets();
        IEnumerable<TempTicket> GetTempTickets(int? ShowID);
        IEnumerable<TempTicket> GetTempTickets(long ReservationID);
        void SaveTempTickets(List<TempTicket> TempTickets);
        void UpdateTempTickets(List<TempTicket> TempTickets);
        void DeleteTempTickets(long ReservationID);
    }
}
