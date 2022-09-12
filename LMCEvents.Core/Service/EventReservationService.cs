using LMCEvents.Core.Interfaces;
using LMCEvents.DTOs;
using LMCEvents.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMCEvents.Core.Service
{
    public class EventReservationService : IEventReservationService
    {
        public IEventReservationRepository _eventReservationRepository;
        public IBookingDTOMapper _bookingDTOMapper;

        public EventReservationService(IEventReservationRepository eventReservationRepository, IBookingDTOMapper bookingDTOMapper)
        {
            _eventReservationRepository = eventReservationRepository;
            _bookingDTOMapper = bookingDTOMapper;
        }

        public List<BookingResponseDTO> GetBookings()
        {
            return _bookingDTOMapper.MapBookingsList(_eventReservationRepository.GetBookings());
        }
    }
}
