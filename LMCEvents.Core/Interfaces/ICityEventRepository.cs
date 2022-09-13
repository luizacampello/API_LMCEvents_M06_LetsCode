using LMCEvents.Core.Model;
using LMCEvents.DTOs;

namespace LMCEvents.Core.Interfaces
{
    public interface ICityEventRepository
    {
        List<CityEvent> GetCityEvents();

        List<CityEvent> GetEventByTitle(string title);

        CityEvent GetEventByTitleAndLocal(string title, string local);

        List<CityEvent> GetEventByLocalAndDate(string local, DateTime date);

        List<CityEvent> GetEventByPriceAndDate(decimal priceMin, decimal priceMax, DateTime date);

        bool InsertEvent(CityEvent cityEvent);

        bool UpdateEvent(long id, CityEvent cityEvent);

        bool DeleteEvent(CityEvent cityEvent);

    }
}
