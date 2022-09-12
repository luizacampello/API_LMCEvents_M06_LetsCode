using Dapper;
using LMCEvents.Core.Interfaces;
using LMCEvents.Core.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LMCEvents.Infra.Data.Repositories
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;

        public CityEventRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool DeleteEvent(CityEvent cityEvent)
        {
            throw new NotImplementedException();
        }

        public List<CityEvent> GetCityEvents()
        {
            string query = "SELECT * FROM CityEvent";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<CityEvent>(query).ToList();
        }

        public CityEvent GetEventByLocalAndDate(string title)
        {
            throw new NotImplementedException();
        }

        public CityEvent GetEventByPriceAndDate(string title)
        {
            throw new NotImplementedException();
        }

        public CityEvent GetEventByTitle(string title)
        {
            string query = "SELECT * FROM CityEvent WHERE title = @title";

            var parameters = new DynamicParameters();
            parameters.Add("title", title);

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

        public bool UpdateEvent(CityEvent cityEvent)
        {
            throw new NotImplementedException();
        }
    }
}
