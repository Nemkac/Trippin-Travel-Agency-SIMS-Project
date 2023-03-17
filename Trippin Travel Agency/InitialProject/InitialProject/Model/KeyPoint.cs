using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class KeyPoint
    {
        public int id { get; set; }

        public string name { get; set; }

        public bool visited { get; set; }

        public KeyPoint(string name, bool visited)
        {
            this.name = name;
            this.visited = visited;
        }
        public KeyPoint() { }   
    }
}
