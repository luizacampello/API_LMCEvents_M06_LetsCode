using LMCEvents.Core.Interfaces;
using LMCEvents.Core.Model;
using LMCEvents.DTOs;

namespace LMCEvents.Core.Service
{
    public class EventReservationService : IEventReservationService
    {
        public IEventReservationRepository _eventReservationRepository;
        public IBookingDTOMapper _bookingDTOMapper;
        public ICityEventRepository _cityEventRepository;

        public EventReservationService(IEventReservationRepository eventReservationRepository, ICityEventRepository cityEventRepository, IBookingDTOMapper bookingDTOMapper)
        {
            _eventReservationRepository = eventReservationRepository;
            _cityEventRepository = cityEventRepository;
            _bookingDTOMapper = bookingDTOMapper;
        }

        public BookingResponseDTO GetBookingByPersonNameAndEventTitle(string personName, string eventTitle)
        {
            throw new NotImplementedException();
        }

        public List<BookingResponseDTO> GetBookings()
        {
            return _bookingDTOMapper.MapBookingsList(_eventReservationRepository.GetBookings());
        }

        public bool InsertBooking(long idEvent, string PersonName, long Quantity)
        {
            //CityEvent bookedEvent = _cityEventRepository.GetEventByTitleAndLocal(title, local);

            //if (bookedEvent == null)
            //{
            //    return false;
            //}

            //EventReservation eventReservation = _bookingDTOMapper.MapResponseDTOToEventReservation(booking, bookedEvent.IdEvent);
            EventReservation eventReservation = new ()
            {
                IdEvent = idEvent,
                PersonName = PersonName,
                Quantity = Quantity
            };

            return _eventReservationRepository.InsertBooking(eventReservation);

        }

        public bool UpdateBooking(BookingResponseDTO booking)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBooking(BookingResponseDTO booking)
        {
            throw new NotImplementedException();
        }
    }
}
