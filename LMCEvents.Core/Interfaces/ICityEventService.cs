using LMCEvents.Core.Model;
using LMCEvents.DTOs;

namespace LMCEvents.Core.Interfaces
{
    public interface ICityEventService
    {
        List<EventResponseDTO> GetCityEvents();

        EventResponseDTO GetEventByTitle(string title);

        EventResponseDTO GetEventByLocalAndDate(string title);

        EventResponseDTO GetEventByPriceAndDate(string title);

        bool InsertEvent(EventResponseDTO eventResponseDTO);

        bool UpdateEvent(EventResponseDTO eventResponseDTO);

        bool DeleteEvent(EventResponseDTO eventResponseDTO);

    }
}
