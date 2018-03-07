using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IMovieOverviewRepository
    {
        List<Entities.Boolean> getMovieList();
        List<Show> getShowList();
        List<Show> getShowbyId(int id);
        IEnumerable<Entities.Boolean> GetMovies();
    }
}
