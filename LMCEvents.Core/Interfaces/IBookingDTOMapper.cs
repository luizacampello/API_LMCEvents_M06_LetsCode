using LMCEvents.Core.Model;
using LMCEvents.DTOs;

namespace LMCEvents.Core.Interfaces
{
    public interface IBookingDTOMapper
    {
        BookingResponseDTO MapEventReservationToResponseDTO(EventReservation eventReservation, CityEvent cityEvent);

        EventReservation MapResponseDTOToEventReservation(BookingResponseDTO booking);
    }
}
