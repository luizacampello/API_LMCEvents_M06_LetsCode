using LMCEvents.Core.Model;
using LMCEvents.DTOs;

namespace LMCEvents.Core.Interfaces
{
    public interface IEventReservationService
    {
        List<BookingResponseDTO> GetBookings();

        BookingResponseDTO GetBookingById(long idBooking);

        List<BookingResponseDTO> GetBookingByPersonNameAndEventTitle(string personName, string eventTitle);

        bool InsertBooking(BookingResponseDTO booking);

        bool UpdateBooking(long idEvent, long quantity);

        bool DeleteBooking(long idEvent);
    }
}
