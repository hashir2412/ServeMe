using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServeMe.Models;
using ServeMe.Repository.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ServeMe.Repository
{
    public class CredsRepository : ICredsRepository
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<CredsRepository> _logger;

        public CredsRepository(ILogger<CredsRepository> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public async Task<bool> UserExists(string email)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var parameters = new { Email = email };
                var sql = "Select email from Creds where email = @Email";
                var result = await connection.QueryFirstOrDefaultAsync<CredsDbModel>(sql, parameters);
                return result == null ? false : true;
            }
        }

        public async Task<bool> Register(string email, string password, bool isCustomer)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var parameters = new { Email = email, Password = password, IsCustomer = isCustomer };
                var sql = "INSERT INTO Creds (Email,Password,IsCustomer) VALUES(@Email, @Password,@IsCustomer)";
                int rowsAffected = await connection.ExecuteAsync(sql, parameters);
                return rowsAffected == 1 ? true : false;
            }
        }

        public async Task<ResponseBaseModel<bool>> Login(string email, string password, bool isCustomer)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var parameters = new { Email = email, Password = password, IsCustomer = isCustomer };
                var sql = "Select * from Creds where email = @Email and password = @Password and isCustomer = @IsCustomer";
                var result = await connection.QueryFirstOrDefaultAsync<CredsDbModel>(sql, parameters);
                if (result == null)
                {
                    return new ResponseBaseModel<bool>() { Body = false, Message = "User does not exist, please register", StatusCode = 1 };
                }
                else
                {
                    if (result.Password == password && isCustomer == result.IsCustomer)
                    {
                        return new ResponseBaseModel<bool>() { Body = result.IsCustomer, Message = "Success", StatusCode = 0 };
                    }
                    else
                    {
                        return new ResponseBaseModel<bool>() { Body = false, Message = "The Email ID and Password dont match.", StatusCode = 2 };
                    }
                }
            }
        }
    }
}
