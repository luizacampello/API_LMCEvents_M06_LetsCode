using LMCEvents.Core.Model;

namespace LMCEvents.Core.Interfaces
{
    public interface IEventReservationRepository
    {
        List<EventReservation> GetBookings();

        EventReservation GetBookingById(long idBooking);

        EventReservation GetBookingByIdEvent(long idEvent);

        List<EventReservation> GetBookingsByPersonNameAndEventTitle(string personName, string eventTitle);

        bool InsertBooking(EventReservation booking);

        bool UpdateBooking(long idBooking, long newQuantity);

        bool DeleteBooking(long idBooking);        

    }
}
