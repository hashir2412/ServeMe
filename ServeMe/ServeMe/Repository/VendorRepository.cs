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
    public class VendorRepository : IVendorRepository
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<VendorRepository> _logger;
        private readonly IMapper _mapper;

        public VendorRepository(ILogger<VendorRepository> logger, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<ResponseBaseModel<VendorDto>> GetVendorDetails(int id)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var parameters = new { UserID = id };
                var sql = "select * from Vendors where VendorID = @UserID";
                var result = await connection.QueryFirstOrDefaultAsync<VendorDbModel>(sql, parameters);
                var userDto = _mapper.Map<VendorDto>(result);
                return result == null ? new ResponseBaseModel<VendorDto>() { Body = null, Message = "Vendor not found", StatusCode = 1 } : new ResponseBaseModel<VendorDto>()
                {
                    Body = userDto,
                    Message = "Success",
                    StatusCode = 0
                };
            }
        }

        public async Task<ResponseBaseModel<VendorDto>> GetVendorDetails(string email)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var parameters = new { Email = email};
                var sql = "select * from Vendors where email = @Email";
                var result = await connection.QueryFirstOrDefaultAsync<VendorDbModel>(sql, parameters);
                var userDto = _mapper.Map<VendorDto>(result);
                return result == null ? new ResponseBaseModel<VendorDto>() { Body = null, Message = "Vendor not found", StatusCode = 1 } : new ResponseBaseModel<VendorDto>()
                {
                    Body = userDto,
                    Message = "Success",
                    StatusCode = 0
                };
            }
        }

        public async Task<ResponseBaseModel<VendorDto>> Login(string email, string password)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var parameters = new { Email = email,Password = password };
                var sql = "select * from Creds where Email = @Email and Password=@Password";
                var result = await connection.QueryFirstOrDefaultAsync<VendorDbModel>(sql, parameters);
                var userDto = _mapper.Map<VendorDto>(result);
                return result == null ? new ResponseBaseModel<VendorDto>() { Body = null, Message = "Vendor not found", StatusCode = 1 } : new ResponseBaseModel<VendorDto>()
                {
                    Body = userDto,
                    Message = "Success",
                    StatusCode = 0
                };
            }
        }

        public async Task<ResponseBaseModel<int>> Register(VendorDto vendor)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var vendorDto = new VendorDto
                {
                    Name = vendor.Name,
                    Phone = vendor.Phone,
                    Email = vendor.Email,
                    ReceiveCommunication = vendor.ReceiveCommunication,
                    Address = vendor.Address,
                    Agreement = vendor.Agreement
                };
                var vendorDbModel = _mapper.Map<VendorDbModel>(vendorDto);
                var sql = "INSERT INTO Vendors (VendorName, Phone,Email,ReceiveCommunication,Address,Agreement) VALUES(@VendorName, @Phone,@Email,@ReceiveCommunication,@Address,@Agreement);SELECT CAST(SCOPE_IDENTITY() as int)";
                var rowsAffected = await connection.QueryFirstOrDefaultAsync<int>(sql, vendorDbModel);
                return rowsAffected > 0 ? new ResponseBaseModel<int>() { Body = rowsAffected, Message = "Successfully Registered", StatusCode = 0 } :
                    new ResponseBaseModel<int>() { Body = -1, Message = "Failed to register", StatusCode = 1 };
            }
        }
    }
}
