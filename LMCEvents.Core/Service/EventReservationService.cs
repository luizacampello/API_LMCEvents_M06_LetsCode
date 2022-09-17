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

        public List<BookingResponseDTO> GetBookings()
        {
            return MapBookingsList(_eventReservationRepository.GetBookings());
        }

        public List<BookingResponseDTO> GetBookingByPersonNameAndEventTitle(string personName, string eventTitle)
        {
            return MapBookingsList(_eventReservationRepository.GetBookingsByPersonNameAndEventTitle(personName, eventTitle));
        }

        public bool InsertBooking(BookingResponseDTO booking)
        {
            CityEvent bookedEvent = _cityEventRepository.GetEventById(booking.GetIdEvent());

            if (bookedEvent is not null && bookedEvent.Status)
            {
                return _eventReservationRepository.InsertBooking(_bookingDTOMapper.MapResponseDTOToEventReservation(booking));
            }

            return false;
        }

        public bool UpdateBooking(long idReservation, long newQuantity)
        {
            return _eventReservationRepository.UpdateBooking(idReservation, newQuantity);
        }

        public bool DeleteBooking(long idReservation)
        {
            return _eventReservationRepository.DeleteBooking(idReservation);
        }

        public BookingResponseDTO GetBookingById(long idBooking)
        {
            EventReservation reservation = _eventReservationRepository.GetBookingById(idBooking);

            return _bookingDTOMapper.MapEventReservationToResponseDTO(reservation);
        }

        private List<BookingResponseDTO> MapBookingsList(List<EventReservation> eventReservations)
        {
            List<BookingResponseDTO> map = new();

            foreach (EventReservation eventReservation in eventReservations)
            {
                CityEvent currentEvent = _cityEventRepository.GetEventById(eventReservation.IdEvent);

                map.Add(_bookingDTOMapper.MapEventReservationToResponseDTO(eventReservation, currentEvent));
            }

            return map;
        }
    }
}
