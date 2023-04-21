using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interfaces
{
    public interface IAccommodationRepository
    {
        public Accommodation GetById(int id);
        public List<int> GetAllByName(string name);
        public List<int> GetAllByCity(string city);
        public List<int> GetAllByGuestsNumber(int guestsNumber);
        public List<int> GetAllByMininumDays(int days);
        public List<int> GetAllByType(string type);
        public List<Booking> GetAccommodationsBookings(List<Booking> bookings, Accommodation accommodation);
        public void Save(Accommodation accommodation);
        public List<Accommodation> GetOwnersAccommodations(int id);
        public AccommodationLocation GetNewLocation(string country, string city);
    }
}
