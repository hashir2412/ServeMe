using AutoMapper;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServeMe.Models;
using ServeMe.Repository.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ServeMe.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<OrderRepository> _logger;
        private readonly IMapper _mapper;

        public OrderRepository(ILogger<OrderRepository> logger, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }
        public async Task<ResponseBaseModel<IEnumerable<OrderDto>>> GetOrdersByUser(int id)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var sql = "select * from Cart inner join Orders on Orders.OrderID = Cart.OrderID " +
                    "inner join Service on Service.ServiceID = Cart.ServiceID where Orders.UserID = @Id";
                var parameters = new { Id = id };

                var SalesCartList = await connection.QueryAsync<CartDbModel, OrderDbModel, ServiceDbModel, CartDbModel>(sql,
                    (cart, order, service) =>
                    {
                        cart.Order = order;
                        cart.Service = service;
                        return cart;
                    }, parameters, splitOn: "OrderID,ServiceID"
                    );
                List<OrderDto> result = new List<OrderDto>();
                var salesCartGroupedList = SalesCartList.GroupBy(u => u.Order.OrderID)
                                      .Select(grp => new { Id = grp.Key, Items = grp.ToList() })
                                      .ToList();
                foreach (var i in salesCartGroupedList)
                {
                    var cart = SalesCartList.FirstOrDefault(res => res.Order.OrderID == i.Id);
                    OrderDto finalCart = new OrderDto();
                    finalCart.Id = i.Id;
                    finalCart.Items = new List<ItemDto>();
                    i.Items.ForEach(item =>
                    {
                        finalCart.Items.Add(_mapper.Map<ItemDto>(item));
                    });
                    finalCart.Address = cart.Order.Address;
                    finalCart.Date = cart.Order.Date;
                    finalCart.Total = cart.Order.Total;
                    result.Add(finalCart);
                }
                return new ResponseBaseModel<IEnumerable<OrderDto>>() { Body = result, Message = "Success", StatusCode = 0 };

                //var parameters = new { UserID = id };
                //var sql = "select * from Orders where UserID = @UserID";
                //var result = await connection.QueryAsync<UserDbModel>(sql, parameters);
                //var userDto = _mapper.Map<UserDto>(result);
                //return result == null ? new ResponseBaseModel<UserDto>() { Body = null, Message = "User not found", StatusCode = 1 } : new ResponseBaseModel<UserDto>()
                //{
                //    Body = userDto,
                //    Message = "Success",
                //    StatusCode = 0
                //};
            }
        }

        public async Task<ResponseBaseModel<int>> PlaceOrder(OrderDbModel order, SqlConnection connection, SqlTransaction transaction)
        {
            var sql = "INSERT INTO Orders (UserID,StatusID,Address,Date, Total) VALUES(@UserID, @StatusID,@Address,@Date,@Total);SELECT CAST(SCOPE_IDENTITY() as int)";
            var rowsAffected = await connection.QueryFirstOrDefaultAsync<int>(sql, order, transaction);
            return rowsAffected > 0 ? new ResponseBaseModel<int>() { Body = rowsAffected, Message = "Successfully Added Order", StatusCode = 0 } :
                new ResponseBaseModel<int>() { Body = -1, Message = "Failed to add order", StatusCode = 1 };
        }

        public async Task<ResponseBaseModel<int>> AddToCart(CartDbModel cart, SqlConnection connection, SqlTransaction transaction)
        {
            var sql = "INSERT INTO Cart (OrderID,StatusID,ServiceID,Rate, Quantity, Date) VALUES(@OrderID, @StatusID,@ServiceID,@Rate,@Quantity,@Date);SELECT CAST(SCOPE_IDENTITY() as int)";
            var rowsAffected = await connection.QueryFirstOrDefaultAsync<int>(sql, cart, transaction);
            return rowsAffected > 0 ? new ResponseBaseModel<int>() { Body = rowsAffected, Message = "Successfully Added to cart", StatusCode = 0 } :
                new ResponseBaseModel<int>() { Body = -1, Message = "Failed to add cart", StatusCode = 1 };
        }
    }
}
