using InitialProject.Context;
using InitialProject.Model;
using InitialProject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using InitialProject.Interfaces;
using InitialProject.Repository;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace InitialProject.Service.TourServices
{
    class TourRequestService
    {
        private readonly ITourRepository iTourRepository;
        public TourRequestService(ITourRepository iTourRepository)
        {
            this.iTourRepository = iTourRepository;
        }

        public List<TourRequest> GetAllFullTourRequests()
        {
            List<TourRequest> requests = new List<TourRequest>();
            DataBaseContext context = new DataBaseContext();
            foreach(TourRequest tr in context.TourRequests.ToList())
            {
                requests.Add(tr);
            }
            return requests;
        }
    }
}
