﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Image
    {
        public int id { get; set; }
        public String imageLink { get; set; }
        public Nullable<int> tourId { get; set; }

        public Image() { }
        public Image(String imageLink, Nullable<int> tourId)
        {
            this.imageLink = imageLink;
            this.tourId = tourId;
        }
    }
}
