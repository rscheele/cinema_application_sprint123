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
    }
}
