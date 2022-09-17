using LMCEvents.Core.Interfaces;
using LMCEvents.Core.Model;
using LMCEvents.DTOs;

namespace LMCEvents.Mappers
{
    public class BookingDTOMapper : IBookingDTOMapper
    {
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

        public BookingResponseDTO MapEventReservationToResponseDTO(EventReservation eventReservation, CityEvent cityEvent)
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

            response.SetEventInfo($"{cityEvent.Title} - {cityEvent.Local}");
            response.SetBookingPrice(cityEvent.Price);            

            return response;
        }

        public EventReservation MapResponseDTOToEventReservation(BookingResponseDTO bookingResponseDTO)
        {
            EventReservation eventReservation = new()
            {
                PersonName = bookingResponseDTO.PersonName,
                Quantity = bookingResponseDTO.Quantity,
                IdEvent = bookingResponseDTO.GetIdEvent(),
            };

            return eventReservation;
        }
    }
}
