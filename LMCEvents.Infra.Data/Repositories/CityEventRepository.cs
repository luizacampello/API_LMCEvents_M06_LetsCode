using Dapper;
using LMCEvents.Core.Interfaces;
using LMCEvents.Core.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;

namespace LMCEvents.Infra.Data.Repositories
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;

        public CityEventRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<CityEvent> GetCityEvents()
        {
            string query = "SELECT * FROM CityEvent";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query).ToList();
        }

        public List<CityEvent> GetEventByLocalAndDate(string local, DateTime date)
        {
            string queryNoDate = "SELECT * FROM CityEvent WHERE local = @local AND status = 1";

            string queryWithDate = "SELECT * FROM CityEvent WHERE local = @local AND CAST(dateHourEvent AS DATE) = CAST(@date AS DATE) AND status = 1";

            string query;

            if (date == default)
            {
                query = queryNoDate;
            }
            else
            {
                query = queryWithDate;
            }

            var parameters = new DynamicParameters();
            parameters.Add("local", local);
            parameters.Add("date", date.ToString("yyyy-MM-ddTHH:mm:ss.fff"));

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public List<CityEvent> GetEventByPriceAndDate(decimal priceMin, decimal priceMax, DateTime date)
        {          
            string queryNoDate = "SELECT * FROM CityEvent WHERE price BETWEEN @priceMin AND @priceMax AND status = 1";

            string queryWithDate = "SELECT * FROM CityEvent WHERE price BETWEEN @priceMin AND @priceMax AND CAST(dateHourEvent AS DATE) = CAST(@date AS DATE) AND status = 1";

            string query;

            if (date == default)
            {
                query = queryNoDate;
            }
            else
            {
                query = queryWithDate;
            }

            var parameters = new DynamicParameters();
            parameters.Add("priceMin", priceMin);
            parameters.Add("priceMax", priceMax);
            parameters.Add("date", date.ToString("yyyy-MM-ddTHH:mm:ss.fff"));

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public List<CityEvent> GetEventByTitle(string title)
        {
            string query = "SELECT * FROM CityEvent WHERE title LIKE CONCAT('%',@title,'%')";

            var parameters = new DynamicParameters();
            parameters.Add("title", title);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query, parameters).ToList();
        }

        public bool InsertEvent(CityEvent newEvent)
        {
            var query = "INSERT INTO CityEvent VALUES (@title, @description, @dateHourEvent, @local, @address, @price, 1)";

            var parameters = new DynamicParameters(new
            {
                newEvent.Title,
                newEvent.Description,
                newEvent.DateHourEvent,
                newEvent.Local,
                newEvent.Address,
                newEvent.Price
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateEvent(CityEvent eventToUpdate)
        {
            string query = "UPDATE CityEvent SET title = @title, description = @description, dateHourEvent = @dateHourEvent, local = @local, address = @address, price = @price WHERE idEvent = @idEvent;";
            
            var parameters = new DynamicParameters(new 
            { 
                eventToUpdate.Title,
                eventToUpdate.Description,
                eventToUpdate.DateHourEvent,
                eventToUpdate.Local,
                eventToUpdate.Address,
                eventToUpdate.Price,
                eventToUpdate.IdEvent

            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool DeleteEvent(long idEvent)
        {
            string query = "DELETE FROM CityEvent WHERE idEvent = @idEvent;";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateEventStatus(long idEvent)
        {
            int newStatus = 0;
            string query = "UPDATE CityEvent SET status = @status WHERE idEvent = @idEvent;";            

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);
            parameters.Add("status", newStatus);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public CityEvent GetEventById(long idEvent)
        {
            string query = "SELECT * FROM CityEvent WHERE idEvent = @idEvent";

            var parameters = new DynamicParameters();
            parameters.Add("idEvent", idEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<CityEvent>(query, parameters);
        }
    }
}
