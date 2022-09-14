using LMCEvents.Core.Interfaces;
using LMCEvents.Core.Model;
using LMCEvents.DTOs;

namespace LMCEvents.Mappers
{
    public class EventDTOMapper : IEventDTOMapper
    {
        public List<EventResponseDTO> MapCityEventsList(List<CityEvent> cityEvents)
        {
            List<EventResponseDTO> map = new();

            foreach (CityEvent cityEvent in cityEvents)
            {
                map.Add(MapCityEventToResponseDTO(cityEvent));
            }

            return map;
        }

        public EventResponseDTO MapCityEventToResponseDTO(CityEvent cityEvent)
        {
            if (cityEvent is null)
            {
                return null;
            }

            EventResponseDTO eventResponseDTO = new()
            {
                Title = cityEvent.Title,
                Description = cityEvent.Description,
                DateHourEvent = cityEvent.DateHourEvent,
                Local = cityEvent.Local,
                Address = cityEvent.Address,
                Price = cityEvent.Price
            };

            return eventResponseDTO;
        }

        public CityEvent MapResponseDTOToCityEvent(EventResponseDTO eventResponse)
        {
            CityEvent cityEvent = new()
            {
                Title = eventResponse.Title,
                Description = eventResponse.Description,
                DateHourEvent = eventResponse.DateHourEvent,
                Local = eventResponse.Local,
                Address = eventResponse.Address,
                Price = eventResponse.Price,
                Status = true
            };

            return cityEvent;
        }
    }
}
