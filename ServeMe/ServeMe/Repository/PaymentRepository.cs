using AutoMapper;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServeMe.Models;
using ServeMe.Repository.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ServeMe.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<PaymentRepository> _logger;
        private readonly IMapper _mapper;

        public PaymentRepository(ILogger<PaymentRepository> logger, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }
        public async Task<ResponseBaseModel<int>> AddPayment(PaymentDbModel model, SqlConnection sqlConnection)
        {
            var sql = "INSERT INTO Payment (PaymentType, OrderID,UserID,TotalAmount,CommissionDeducted,Date) VALUES(@PaymentType, @OrderID,@UserID,@TotalAmount,@CommissionDeducted,@Date);SELECT CAST(SCOPE_IDENTITY() as int)";
            var rowsAffected = await sqlConnection.QueryFirstOrDefaultAsync<int>(sql, model);
            return rowsAffected > 0 ? new ResponseBaseModel<int>() { Body = rowsAffected, Message = "Successfully Added Payment", StatusCode = 0 } :
                new ResponseBaseModel<int>() { Body = -1, Message = "Failed to add payment", StatusCode = 1 };
        }
    }
}
