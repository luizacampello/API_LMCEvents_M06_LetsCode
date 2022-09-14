using LMCEvents.Core.Model;

namespace LMCEvents.Core.Interfaces
{
    public interface IEventReservationRepository
    {
        List<EventReservation> GetBookings();

        EventReservation GetBookingById(long idBooking);

        List<EventReservation> GetBookingByPersonNameAndEventTitle(string personName, string eventTitle);

        bool InsertBooking(EventReservation booking);

        bool UpdateBooking(long idBooking, EventReservation booking);

        bool DeleteBooking(long idBooking);

    }
}
