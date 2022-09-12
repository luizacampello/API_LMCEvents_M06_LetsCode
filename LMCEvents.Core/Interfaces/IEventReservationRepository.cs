using LMCEvents.Core.Model;
using LMCEvents.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMCEvents.Core.Interfaces
{
    public interface IEventReservationRepository
    {
        List<EventReservation> GetBookings();

        EventReservation GetBookingByPersonNameAndEventTitle(string personName, string eventTitle);

        bool InsertBooking(EventReservation booking);

        bool UpdateBooking(EventReservation booking);

        bool DeleteBooking(EventReservation booking);

    }
}
