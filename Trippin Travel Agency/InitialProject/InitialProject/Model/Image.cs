using Microsoft.EntityFrameworkCore;
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
        public int tourId { get; set; }

        /*public Image(int id, String imageLink, int tourId) { 
            this.id = id;
            this.imageLink = imageLink;
            this.tourId = tourId;
        }*/
    }
}
