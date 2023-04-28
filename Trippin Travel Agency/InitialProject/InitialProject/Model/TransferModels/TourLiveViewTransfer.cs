﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model.TransferModels
{
    public class TourTodayImagesTransfer
    {
        public int id { get; set; }
        public int tourId { get; set; } 

        public TourTodayImagesTransfer(int tourId)
        {
            this.tourId = tourId; 
        }

    }
}
