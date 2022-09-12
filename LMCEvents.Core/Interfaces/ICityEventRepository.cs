using LMCEvents.Core.Model;
using LMCEvents.DTOs;

namespace LMCEvents.Core.Interfaces
{
    public interface ICityEventRepository
    {
        List<CityEvent> GetCityEvents();

        CityEvent GetEventByTitle(string title);

        CityEvent GetEventByLocalAndDate(string title);

        CityEvent GetEventByPriceAndDate(string title);

        bool InsertEvent(CityEvent cityEvent);

        bool UpdateEvent(CityEvent cityEvent);

        bool DeleteEvent(CityEvent cityEvent);

    }
}
