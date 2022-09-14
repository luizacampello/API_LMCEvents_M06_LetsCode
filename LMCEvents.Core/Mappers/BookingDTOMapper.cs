using LMCEvents.Core.Interfaces;
using LMCEvents.Core.Model;
using LMCEvents.DTOs;

namespace LMCEvents.Mappers
{
    public class BookingDTOMapper : IBookingDTOMapper
    {
        public List<BookingResponseDTO> MapBookingsList(List<EventReservation> eventReservations)
        {
            List<BookingResponseDTO> map = new();

            foreach (EventReservation eventReservation in eventReservations)
            {
                map.Add(MapEventReservationToResponseDTO(eventReservation));
            }

            return map;
        }

        public BookingResponseDTO MapEventReservationToResponseDTO(EventReservation eventReservation)
        {
            if (eventReservation is null)
            {
                return null;
            }

            BookingResponseDTO response = new()
            {
                PersonName = eventReservation.PersonName,
                Quantity = eventReservation.Quantity,
                IdEvent = eventReservation.IdEvent,
            };

            return response;
        }

        public EventReservation MapResponseDTOToEventReservation(BookingResponseDTO bookingResponseDTO)
        {
            EventReservation eventReservation = new()
            {
                PersonName = bookingResponseDTO.PersonName,
                Quantity = bookingResponseDTO.Quantity,
                IdEvent = bookingResponseDTO.IdEvent
            };

            return eventReservation;
        }
    }
}
