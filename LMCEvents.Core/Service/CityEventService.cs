using LMCEvents.Core.Interfaces;
using LMCEvents.Core.Model;
using LMCEvents.DTOs;

namespace LMCEvents.Core.Service
{
    public class CityEventService : ICityEventService
    {
        public ICityEventRepository _cityEventRepository;
        public IEventDTOMapper _eventDTOMapper;
        public IEventReservationRepository _eventReservationRepository;

        public CityEventService(ICityEventRepository cityEventRepository,IEventReservationRepository eventReservationRepository, IEventDTOMapper eventDTOMapper)
        {
            _cityEventRepository = cityEventRepository;
            _eventReservationRepository = eventReservationRepository;
            _eventDTOMapper = eventDTOMapper;
        }

        public List<EventResponseDTO> GetCityEventsByLocal(string local)
        {
            return _eventDTOMapper.MapCityEventsList(_cityEventRepository.GetCityEventsByLocal(local));
        }

        public EventResponseDTO GetEventById(long idEvent)
        {
            return _eventDTOMapper.MapCityEventToResponseDTO(_cityEventRepository.GetEventById(idEvent));
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

        public bool UpdateEvent(long idEvent, EventResponseDTO eventResponseDTO)
        {
            CityEvent eventToUpdate = _eventDTOMapper.MapResponseDTOToCityEvent(eventResponseDTO);
            eventToUpdate.IdEvent = idEvent;

            return _cityEventRepository.UpdateEvent(eventToUpdate);
        }

        public bool DeleteEvent(long idEvent)
        {
            if (_eventReservationRepository.GetBookingByIdEvent(idEvent) is null)
            {
                return _cityEventRepository.DeleteEvent(idEvent);
            }

            return _cityEventRepository.UpdateEventStatus(idEvent);
        }
    }
}
