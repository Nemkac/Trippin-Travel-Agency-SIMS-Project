using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    
    public class TourKeyPoints
    {
        public int id { get; set; }
        public KeyPoint start { get; set; } 
        public KeyPoint end { get; set; }   
        
        public List<KeyPoint> checkpoints = new List<KeyPoint>();

        public TourKeyPoints(int id, KeyPoint start, KeyPoint end, List<KeyPoint> checkpoints)
        {
            this.id = id;
            this.start = start;
            this.end = end;
            this.checkpoints = checkpoints;
        }
        public TourKeyPoints() { }
    }
}
