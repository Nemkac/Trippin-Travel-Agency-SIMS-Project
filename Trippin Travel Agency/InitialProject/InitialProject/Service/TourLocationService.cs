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
            DataBaseContext db = new DataBaseContext();
            TourLocation matchingTourLocation = new TourLocation();

            foreach(TourLocation tourLocation in db.TourLocation.ToList())
            {
                if(tourLocation.id == id)
                {
                    matchingTourLocation = tourLocation;
                }

            }

            return matchingTourLocation;
        }


    }
}
