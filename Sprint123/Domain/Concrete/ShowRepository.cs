using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class ShowRepository : IShowRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Show> GetShows()
        {
            return context.Shows;
        }

        public void SaveShow(Show show)
        {
            if (show.ShowID == 0)
            {
                context.Shows.Add(show);
            }
            else
            {
                Show dbEntry = context.Shows.Find(show.ShowID);
                if (dbEntry != null)
                {
                    dbEntry.BeginTime = show.BeginTime;
                    dbEntry.EndTime = show.EndTime;
                    dbEntry.MovieID = show.MovieID;
                    dbEntry.RoomID = show.RoomID;
                    dbEntry.NumberofTickets = show.NumberofTickets;
                    dbEntry.ChildDiscount = show.ChildDiscount;
                    dbEntry.StudentDiscount = show.StudentDiscount;
                    dbEntry.SeniorDiscount = show.SeniorDiscount;
                }
            }
            context.SaveChanges();
        }
    }
}
