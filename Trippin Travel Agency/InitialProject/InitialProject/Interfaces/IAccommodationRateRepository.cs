using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interfaces
{
    public interface IAccommodationRateRepository
    {
        public void Save(AccommodationRate accommodationRate);
    }
}
