using InitialProject.Context;
using InitialProject.Model;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class TourRepository
    {
        public Tour GetById(int id)
        {
            using (var db = new DataBaseContext())
            {
                foreach (Tour tour in db.Tours) 
                {
                    if (tour.id == id) { 
                    
                    return tour;
                    
                    }
                }
                return null;    
            }
        }
    }
}
