using InitialProject.Context;
using InitialProject.DTO;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Service
{
    public class TourReviewService
    {
        public TourAndGuideRateDTO transformTourReviewToDTO(TourAndGuideRate tagr, int kpid )
        {
            TourAndGuideRateDTO tourReviewsDTO = new TourAndGuideRateDTO(tagr.id, kpid, tagr.guestId, tagr.guideKnowledgeRating, tagr.guideLanguageUsageRating, tagr.contentRating, tagr.valid);
            return tourReviewsDTO;
        }

        public TourAndGuideRate GetById(int id)
        {
            using DataBaseContext dbContext = new DataBaseContext();
            return dbContext.TourAndGuideRates.SingleOrDefault(t => t.id == id);
        }

        public List<TourAndGuideRate> GetReviewsById(int tourid)
        {
            List<TourAndGuideRate> reviews = new List<TourAndGuideRate>(); 
            DataBaseContext context = new DataBaseContext();
            foreach(TourAndGuideRate tagr in context.TourAndGuideRates.ToList())
            { 
                if(tagr.tourId == tourid)
                {
                    reviews.Add(tagr);
                }
            }
            return reviews; 
        }
    }
}
