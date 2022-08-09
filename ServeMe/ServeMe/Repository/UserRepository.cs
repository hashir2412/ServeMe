using AutoMapper;
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
    public class UserRepository : IUserRepository
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<UserRepository> _logger;
        private readonly IMapper _mapper;

        public UserRepository(ILogger<UserRepository> logger, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<ResponseBaseModel<int>> AddReview(ReviewsRatingsRequestModel value)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var reviewRatingDbModel = _mapper.Map<ReviewsRatingsDbModel>(value);
                reviewRatingDbModel.Date = System.DateTime.Now;
                var sql = "INSERT INTO ReviewsRatings (VendorId, CartID,UserID,Comment,Stars, Date) VALUES(@VendorId, @CartID,@UserID,@Comment,@Stars, @Date);SELECT CAST(SCOPE_IDENTITY() as int)";
                var idOfNewRow = await connection.QueryFirstOrDefaultAsync<int>(sql, reviewRatingDbModel);
                var paramter = new { idOfNewRow = idOfNewRow,CartID = value.CartId };
                var sql2 = "Update Cart set ReviewRatingId=@idOfNewRow where CartId = @CartID;";
                var idOfNewRow2 = await connection.ExecuteAsync(sql2, paramter);
                return idOfNewRow > 0 ? new ResponseBaseModel<int>() { Body = idOfNewRow, Message = "Successfully Added Review", StatusCode = 0 } :
                    new ResponseBaseModel<int>() { Body = -1, Message = "Failed to add review", StatusCode = 1 };
            }
        }

        public async Task<ResponseBaseModel<UserDto>> GetUserDetails(int id)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var parameters = new { UserID = id };
                var sql = "select * from Users where UserID = @UserID";
                var result = await connection.QueryFirstOrDefaultAsync<UserDbModel>(sql, parameters);
                var userDto = _mapper.Map<UserDto>(result);
                return result == null ? new ResponseBaseModel<UserDto>() { Body = null, Message = "User not found", StatusCode = 1 } : new ResponseBaseModel<UserDto>()
                {
                    Body = userDto,
                    Message = "Success",
                    StatusCode = 0
                };
            }
        }

        public async Task<ResponseBaseModel<UserDto>> GetUserDetails(string email)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var parameters = new { Email = email };
                var sql = "select * from Users where email = @Email";
                var result = await connection.QueryFirstOrDefaultAsync<UserDbModel>(sql, parameters);
                var userDto = _mapper.Map<UserDto>(result);
                return result == null ? new ResponseBaseModel<UserDto>() { Body = null, Message = "User not found", StatusCode = 1 } : new ResponseBaseModel<UserDto>()
                {
                    Body = userDto,
                    Message = "Success",
                    StatusCode = 0
                };
            }
        }

        public async Task<ResponseBaseModel<int>> Register(UserDto user)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                //var userDto = new UserDto { Name = user.Name, Phone = user.Phone, Email = user.Email, ReceiveCommunication = user.ReceiveCommunication, Point = user.Point };
                var userDbModel = _mapper.Map<UserDbModel>(user);
                var sql = "INSERT INTO Users (UserName, Phone,Email,ReceiveCommunication,Point) VALUES(@UserName, @Phone,@Email,@ReceiveCommunication,@Point);SELECT CAST(SCOPE_IDENTITY() as int)";
                var idOfNewRow = await connection.QueryFirstOrDefaultAsync<int>(sql, userDbModel);
                return idOfNewRow > 0 ? new ResponseBaseModel<int>() { Body = idOfNewRow, Message = "Successfully Registered", StatusCode = 0 } :
                    new ResponseBaseModel<int>() { Body = -1, Message = "Failed to register", StatusCode = 1 };
            }
        }
    }
}
