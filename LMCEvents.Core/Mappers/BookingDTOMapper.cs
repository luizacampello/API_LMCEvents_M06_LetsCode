using LMCEvents.Core.Interfaces;
using LMCEvents.Core.Model;
using LMCEvents.DTOs;

namespace LMCEvents.Mappers
{
    public class BookingDTOMapper : IBookingDTOMapper
    {
        private readonly ICityEventRepository _cityEventRepository;

        public BookingDTOMapper(ICityEventRepository cityEventRepository)
        {
            _cityEventRepository = cityEventRepository;
        }

        public List<BookingResponseDTO> MapBookingsList(List<EventReservation> eventReservations)
        {
            List<BookingResponseDTO> map = new();

            foreach (EventReservation eventReservation in eventReservations)
            {
                CityEvent currentEvent = _cityEventRepository.GetEventById(eventReservation.IdEvent);

                map.Add(MapEventReservationToResponseDTO(eventReservation, currentEvent));
            }

            return map;
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

            response.SetBookingPrice(cityEvent.Price);
            response.SetTitle(cityEvent.Title);

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
