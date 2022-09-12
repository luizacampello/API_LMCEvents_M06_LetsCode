using LMCEvents.Core.Interfaces;
using LMCEvents.Core.Model;
using LMCEvents.DTOs;

namespace LMCEvents.Mappers
{
    public class BookingDTOMapper : IBookingDTOMapper
    {
        public List<BookingResponseDTO> MapBookingsList(List<EventReservation> eventReservations)
        {
            throw new NotImplementedException();
        }

        public BookingResponseDTO MapEventReservationToResponseDTO(EventReservation eventReservation, CityEvent bookedEvent)
        {
            BookingResponseDTO response = new()
            {
                PersonName = eventReservation.PersonName,
                Quantity = eventReservation.Quantity,
                EventTitle = bookedEvent.Title,
                EventDescription = bookedEvent.Description,
                EventDate = bookedEvent.DateHourEvent,
                EventLocal = bookedEvent.Local,
                EventAddress = bookedEvent.Address,
                BookingPrice = (bookedEvent.Price * eventReservation.Quantity)
            };

            return response;
        }

        public EventReservation MapResponseDTOToEventReservation(BookingResponseDTO bookingResponseDTO)
        {
            throw new NotImplementedException();
        }
    }
}
