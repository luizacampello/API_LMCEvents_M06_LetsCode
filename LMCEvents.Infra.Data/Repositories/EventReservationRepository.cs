using Dapper;
using LMCEvents.Core.Interfaces;
using LMCEvents.Core.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LMCEvents.Infra.Data.Repositories
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly IConfiguration _configuration;

        public EventReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public EventReservation GetBookingByPersonNameAndEventTitle(string personName, string eventTitle)
        {
            throw new NotImplementedException();
        }

        public List<EventReservation> GetBookings()
        {
            string query = "SELECT * FROM EventReservation";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<EventReservation>(query).ToList();
        }

        public bool InsertBooking(EventReservation booking)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBooking(EventReservation booking)
        {
            string query = "UPDATE EventReservation SET Quantity = @quantity WHERE PersonName = @PersonName and idEvent = @idEvent;";
;
            var parameters = new DynamicParameters(booking);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool DeleteBooking(EventReservation booking)
        {
            throw new NotImplementedException();
        }
    }
}
