using LMCEvents.DTOs;

namespace LMCEvents.Core.Interfaces
{
    public interface ICityEventService
    {
        List<EventResponseDTO> GetCityEvents();

        EventResponseDTO GetEventById(long idEvent);

        List<EventResponseDTO> GetEventByTitle(string title);

        List<EventResponseDTO> GetEventByLocalAndDate(string local, DateTime date);

        List<EventResponseDTO> GetEventByPriceAndDate(decimal priceMin, decimal priceMax, DateTime date);

        bool InsertEvent(EventResponseDTO eventResponseDTO);

        bool UpdateEvent(long idEvent, EventResponseDTO eventResponseDTO);

        bool DeleteEvent(long idEvent);

    }
}
