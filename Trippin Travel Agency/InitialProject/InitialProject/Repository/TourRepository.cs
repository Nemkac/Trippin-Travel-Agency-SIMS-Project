using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class TourRepository
    {
        private List<Tour> _tours;

        public TourRepository()
        {
            _tours = SqliteDataAccess.LoadTours();
        }

        public Tour GetById(int id)
        {
            _tours = SqliteDataAccess.LoadTours();
            return _tours.FirstOrDefault(t => t.id == id);
        }
    }
}
