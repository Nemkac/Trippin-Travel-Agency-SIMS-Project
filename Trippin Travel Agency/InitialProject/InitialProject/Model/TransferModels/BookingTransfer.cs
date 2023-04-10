using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model.TransferModels
{
    public class BookingTransfer
    {
        public int Id { get; set; }
        public int bookingId { get; set; }  
        public int guestId { get; set; }

        public BookingTransfer(int bookingId, int guestId)
        {
            this.bookingId = bookingId;
            this.guestId = guestId;
        }

        public BookingTransfer()
        {
        }
    }
}
