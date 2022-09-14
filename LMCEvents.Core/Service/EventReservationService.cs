using LMCEvents.Core.Interfaces;
using LMCEvents.Core.Model;
using LMCEvents.DTOs;

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

        public List<BookingResponseDTO> GetBookingByPersonNameAndEventTitle(string personName, string eventTitle)
        {
            return _bookingDTOMapper.MapBookingsList(_eventReservationRepository.GetBookingByPersonNameAndEventTitle(personName, eventTitle));
        }

        public List<BookingResponseDTO> GetBookings()
        {
            return _bookingDTOMapper.MapBookingsList(_eventReservationRepository.GetBookings());
        }

        public bool InsertBooking(BookingResponseDTO booking)
        {           
            return _eventReservationRepository.InsertBooking(_bookingDTOMapper.MapResponseDTOToEventReservation(booking));
        }

        public bool UpdateBooking(long idEvent, BookingResponseDTO booking)
        {
            return _eventReservationRepository.UpdateBooking(idEvent, _bookingDTOMapper.MapResponseDTOToEventReservation(booking));
        }

        public bool DeleteBooking(long idEvent)
        {
            return _eventReservationRepository.DeleteBooking(idEvent);
        }

        public BookingResponseDTO GetBookingById(long idBooking)
        {
            return _bookingDTOMapper.MapEventReservationToResponseDTO(_eventReservationRepository.GetBookingById(idBooking));
        }
    }
}
