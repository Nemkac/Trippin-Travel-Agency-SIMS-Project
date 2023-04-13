using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interfaces
{
    internal interface IGuestRateRepository
    {
        public void Save(GuestRate guestRate);
        public bool IsGuestRated(int bookingId);
    }
}
