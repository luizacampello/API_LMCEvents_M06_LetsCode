using LMCEvents.Core.Interfaces;
using LMCEvents.DTOs;

namespace LMCEvents.Core.Service
{
    public class CityEventService : ICityEventService
    {
        public ICityEventRepository _cityEventRepository;
        public IEventDTOMapper _eventDTOMapper;

        public CityEventService(ICityEventRepository cityEventRepository, IEventDTOMapper eventDTOMapper)
        {
            _cityEventRepository = cityEventRepository;
            _eventDTOMapper = eventDTOMapper;
        }

        public bool DeleteEvent(EventResponseDTO eventResponseDTO)
        {
            throw new NotImplementedException();
        }

        public List<EventResponseDTO> GetCityEvents()
        {
            return _eventDTOMapper.MapCityEventsList(_cityEventRepository.GetCityEvents());
        }

        public List<EventResponseDTO> GetEventByLocalAndDate(string local, DateTime date)
        {
            return _eventDTOMapper.MapCityEventsList(_cityEventRepository.GetEventByLocalAndDate(local, date));
        }

        public List<EventResponseDTO> GetEventByPriceAndDate(decimal priceMin, decimal priceMax, DateTime date)
        {
            return _eventDTOMapper.MapCityEventsList(_cityEventRepository.GetEventByPriceAndDate(priceMin, priceMax, date));
        }

        public List<EventResponseDTO> GetEventByTitle(string title)
        {
            return _eventDTOMapper.MapCityEventsList(_cityEventRepository.GetEventByTitle(title));
        }

        public bool InsertEvent(EventResponseDTO newEvent)
        {
            return _cityEventRepository.InsertEvent(_eventDTOMapper.MapResponseDTOToCityEvent(newEvent));
        }

        public bool UpdateEvent(EventResponseDTO eventResponseDTO)
        {
            throw new NotImplementedException();
        }
    }
}
