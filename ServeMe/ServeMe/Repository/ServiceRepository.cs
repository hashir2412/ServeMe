using AutoMapper;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServeMe.Models;
using ServeMe.Repository.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ServeMe.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<ServiceRepository> _logger;
        private readonly IMapper _mapper;

        public ServiceRepository(ILogger<ServiceRepository> logger, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }
        public async Task<ResponseBaseModel<IEnumerable<ServiceCategoryDto>>> GetServices()
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var sql = "select * from ServiceCategory";
                var result = await connection.QueryAsync<ServiceCategoryDbModel>(sql);
                var serviceDto = _mapper.Map<IEnumerable<ServiceCategoryDto>>(result);
                return new ResponseBaseModel<IEnumerable<ServiceCategoryDto>>()
                {
                    Body = serviceDto,
                    Message = "Success",
                    StatusCode = 0
                };
            }
        }

        public async Task<ResponseBaseModel<IEnumerable<ServiceDto>>> GetServicesByVendor(int id)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var sql = "select * from Service where ServiceID=@id";
                var result = await connection.QueryAsync<ServiceDbModel>(sql, id);
                var serviceDto = _mapper.Map<IEnumerable<ServiceDto>>(result);
                return new ResponseBaseModel<IEnumerable<ServiceDto>>()
                {
                    Body = serviceDto,
                    Message = "Success",
                    StatusCode = 0
                };
            }
        }

        public async Task<ResponseBaseModel<int>> Register(ServiceDto service)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var serviceDbModel = _mapper.Map<ServiceDbModel>(service);
                var sql = "INSERT INTO Service (VendorID,ServiceCategoryID,Status) VALUES(@VendorId,@ServiceCategoryId,1);SELECT CAST(SCOPE_IDENTITY() as int)";
                var rowsAffected = await connection.QueryFirstOrDefaultAsync<int>(sql, serviceDbModel);
                return rowsAffected > 0 ? new ResponseBaseModel<int>() { Body = rowsAffected, Message = "Successfully Added", StatusCode = 0 } :
                    new ResponseBaseModel<int>() { Body = -1, Message = "Failed to add service", StatusCode = 1 };
            }
        }

        public async Task<ResponseBaseModel<int>> Update(ServiceDto service)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var serviceDbModel = _mapper.Map<ServiceDbModel>(service);
                var sql = "UPDATE Service SET Status = @Status WHERE ServiceID=@ServiceID;";
                var rowsAffected = await connection.QueryFirstOrDefaultAsync<int>(sql, serviceDbModel);
                return rowsAffected > 0 ? new ResponseBaseModel<int>() { Body = rowsAffected, Message = "Successfully Added", StatusCode = 0 } :
                    new ResponseBaseModel<int>() { Body = -1, Message = "Failed to add service", StatusCode = 1 };
            }
        }
    }
}
