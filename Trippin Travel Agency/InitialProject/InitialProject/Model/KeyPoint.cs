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

        public KeyPoint(int id, string name, bool visited)
        {
            this.id = id;
            this.name = name;
            this.visited = visited;
        }
        public KeyPoint() { }   
    }
}
