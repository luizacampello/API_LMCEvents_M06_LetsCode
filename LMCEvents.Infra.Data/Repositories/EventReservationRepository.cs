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

        public List<EventReservation> GetBookings()
        {
            string query = "SELECT * FROM EventReservation";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<EventReservation>(query).ToList();
        }

        public List<EventReservation> GetBookingsByPersonNameAndEventTitle(string personName, string title)
        {
            string query = @"SELECT er.IdReservation, er.IdEvent, er.PersonName, er.Quantity FROM EventReservation AS er 
                            INNER JOIN CityEvent AS ce ON ce.IdEvent = er.IdEvent 
                            WHERE personName = @personName AND title LIKE CONCAT('%',@title,'%')";

            var parameters = new DynamicParameters();
            parameters.Add("personName", personName);
            parameters.Add("title", title);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<EventReservation>(query, parameters).ToList();
        }               

        public EventReservation GetBookingById(long idBooking)
        {
            string query = "SELECT * FROM EventReservation WHERE idReservation = @id";

            var parameters = new DynamicParameters();
            parameters.Add("id", idBooking);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<EventReservation>(query, parameters);
        }

        public EventReservation GetBookingByIdEvent(long idEvent)
        {
            string query = "SELECT * FROM EventReservation WHERE idEvent = @idEvent";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<EventReservation>(query, parameters);
        }

        public bool InsertBooking(EventReservation booking)
        {
            var query = "INSERT INTO EventReservation VALUES (@IdEvent, @PersonName, @Quantity)";

            var parameters = new DynamicParameters(booking);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateBooking(long idReservation, int newQuantity)
        {
            string query = "UPDATE EventReservation SET Quantity = @quantity WHERE idReservation = @idReservation;";
;
            var parameters = new DynamicParameters();
            parameters.Add("idReservation", idReservation);
            parameters.Add("Quantity", newQuantity);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool DeleteBooking(long idReservation)
        {
            string query = "DELETE FROM EventReservation WHERE idReservation = @idReservation;";

            var parameters = new DynamicParameters();
            parameters.Add("idReservation", idReservation);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }
       
    }
}
