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
            return _bookingDTOMapper.MapBookingsList(_eventReservationRepository.GetBookings());
        }

        public List<BookingResponseDTO> GetBookingByPersonNameAndEventTitle(string personName, string eventTitle)
        {
            return _bookingDTOMapper.MapBookingsList(_eventReservationRepository.GetBookingsByPersonNameAndEventTitle(personName, eventTitle));
        }             

        public bool InsertBooking(BookingResponseDTO booking)
        {           
            return _eventReservationRepository.InsertBooking(_bookingDTOMapper.MapResponseDTOToEventReservation(booking));
        }

        public bool UpdateBooking(long idReservation, int newQuantity)
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
            if (reservation is null)
            {
                return null;
            }

            CityEvent getEvent = _cityEventRepository.GetEventById(reservation.IdEvent);

            return _bookingDTOMapper.MapEventReservationToResponseDTO(reservation, getEvent);
        }
    }
}
