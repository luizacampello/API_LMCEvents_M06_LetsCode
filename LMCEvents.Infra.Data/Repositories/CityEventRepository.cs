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

            string queryWithDate = "SELECT * FROM CityEvent WHERE local = @local AND dateHourEvent = @date AND status = 1";

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

            string queryWithDate = "SELECT * FROM CityEvent WHERE price BETWEEN @priceMin AND @priceMax AND dateHourEvent = @date AND status = 1";

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

        public CityEvent GetEventByTitleAndLocal(string title, string local)
        {
            string query = "SELECT * FROM CityEvent WHERE title = @title AND local = @local)";

            var parameters = new DynamicParameters();
            parameters.Add("title", title);
            parameters.Add("local", local);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<CityEvent>(query, parameters);
        }

        public bool InsertEvent(CityEvent newEvent)
        {
            var query = "INSERT INTO CityEvent VALUES (@title, @description, @dateHourEvent, @local, @adress, @price)";

            var parameters = new DynamicParameters();
            parameters.Add("title", newEvent.Title);
            parameters.Add("description", newEvent.Description);
            parameters.Add("dateHourEvent", newEvent.DateHourEvent);
            parameters.Add("local", newEvent.Local);
            parameters.Add("adress", newEvent.Address);
            parameters.Add("price", newEvent.Price);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateEvent(long id, CityEvent cityEvent)
        {
            string query = "UPDATE CityEvent SET title = @title, description = @description, dateHourEvent = @dateHourEvent, local = @local, adress = @adress, price = @price WHERE idEvent = @idEvent;";

            cityEvent.IdEvent = id;           ;
            var parameters = new DynamicParameters(cityEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool DeleteEvent(CityEvent cityEvent)
        {
            throw new NotImplementedException();
        }

    }
}
