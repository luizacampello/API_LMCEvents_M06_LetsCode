using LMCEvents.Core.Model;
using LMCEvents.DTOs;

namespace LMCEvents.Core.Interfaces
{
    public interface IBookingDTOMapper
    {
        List<BookingResponseDTO> MapBookingsList(List<EventReservation> eventReservations);

        BookingResponseDTO MapEventReservationToResponseDTO(EventReservation eventReservation);

        EventReservation MapResponseDTOToEventReservation(BookingResponseDTO booking);
    }
}
