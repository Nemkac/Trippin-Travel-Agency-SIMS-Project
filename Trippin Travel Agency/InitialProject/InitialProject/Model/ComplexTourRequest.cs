using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class ComplexTourRequest
    {
        public int id { get; set; }

        public List<TourRequest> singleRequestIds { get; set; }

        public ComplexTourRequest(List<TourRequest> singleRequestIds)
        {
            this.singleRequestIds = singleRequestIds;
        }
        public ComplexTourRequest() { }
    }
}
