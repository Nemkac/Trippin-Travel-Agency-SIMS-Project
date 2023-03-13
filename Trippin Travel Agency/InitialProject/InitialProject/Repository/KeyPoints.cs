using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class KeyPoints
    {
        public int id { get; set; } 

        public List<int> ids = new List<int>();

        public KeyPoints() { }

        public KeyPoints(int id, List<int> ids)
        {
            this.id = id;
            this.ids = ids;
        }
    }
}
