using LMCEvents.Core.Model;
using LMCEvents.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMCEvents.Core.Interfaces
{
    public interface IEventDTOMapper
    {
        List<EventResponseDTO> MapCityEventsList(List<CityEvent> cityEvents);

        EventResponseDTO MapCityEventToResponseDTO(CityEvent cityEvent);

        CityEvent MapResponseDTOToCityEvent(EventResponseDTO eventModel);
    }
}
