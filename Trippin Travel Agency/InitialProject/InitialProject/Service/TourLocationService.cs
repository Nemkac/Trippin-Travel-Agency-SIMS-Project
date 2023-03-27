using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class TourLocationService
    {
        public TourLocation GetById(int id) 
        {
            using DataBaseContext db = new DataBaseContext();
            return db.TourLocation.SingleOrDefault(tl => tl.id == id);
        }
    }
}
