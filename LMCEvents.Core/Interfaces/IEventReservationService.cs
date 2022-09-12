using LMCEvents.DTOs;

namespace LMCEvents.Core.Interfaces
{
    public interface IEventReservationService
    {
        List<BookingResponseDTO> GetBookings();

        BookingResponseDTO GetBookingByPersonNameAndEventTitle(string personName, string eventTitle);

        bool InsertBooking(BookingResponseDTO booking);

        bool UpdateBooking(BookingResponseDTO booking);

        bool DeleteBooking(BookingResponseDTO booking);
    }
}
