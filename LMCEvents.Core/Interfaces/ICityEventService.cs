using LMCEvents.Core.Model;
using LMCEvents.DTOs;

namespace LMCEvents.Core.Interfaces
{
    public interface ICityEventService
    {
        List<EventResponseDTO> GetCityEvents();

        List<EventResponseDTO> GetEventByTitle(string title);

        List<EventResponseDTO> GetEventByLocalAndDate(string local, DateTime date);

        List<EventResponseDTO> GetEventByPriceAndDate(decimal priceMin, decimal priceMax, DateTime date);

        bool InsertEvent(EventResponseDTO eventResponseDTO);

        bool UpdateEvent(EventResponseDTO eventResponseDTO);

        bool DeleteEvent(EventResponseDTO eventResponseDTO);

    }
}
