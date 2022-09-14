using LMCEvents.Core.Model;
using LMCEvents.DTOs;

namespace LMCEvents.Core.Interfaces
{
    public interface ICityEventRepository
    {
        List<CityEvent> GetCityEvents();

        CityEvent GetEventById(long idEvent);

        List<CityEvent> GetEventByTitle(string title);

        List<CityEvent> GetEventByLocalAndDate(string local, DateTime date);

        List<CityEvent> GetEventByPriceAndDate(decimal priceMin, decimal priceMax, DateTime date);

        bool InsertEvent(CityEvent cityEvent);

        bool UpdateEvent(CityEvent cityEvent);

        bool DeleteEvent(CityEvent cityEvent);

    }
}
