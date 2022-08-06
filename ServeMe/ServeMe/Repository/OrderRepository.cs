using AutoMapper;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServeMe.Models;
using ServeMe.Repository.Models;
using System;
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
                var sql = "select * from Cart inner join Orders on Orders.OrderID = Cart.OrderID inner join ServiceCategory on Cart.ServiceCategoryId = ServiceCategory.ServiceCategoryID " +
                    "left join Bid on Bid.CartId = Cart.CartId where Orders.UserID = @Id";
                var parameters = new { Id = id };

                var SalesCartList = await connection.QueryAsync<CartDbModel, OrderDbModel, ServiceCategoryDbModel, BidDbModel, CartDbModel>(sql,
                    (cart, order, servicecategory, bid) =>
                    {
                        //if (bid != null)
                        //{
                        //    cart.Bids.Add(bid);
                        //}
                        cart.Order = order;
                        cart.ServiceCategory = servicecategory;
                        if (bid != null)
                        {
                            cart.Bids.Add(bid);
                        }
                        return cart;
                    }, parameters, splitOn: "OrderID,ServiceCategoryID,BidId"
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
                    finalCart.Items = new List<CartDto>();
                    i.Items.ForEach(item =>
                    {
                        finalCart.Items.Add(_mapper.Map<CartDto>(item));
                    });
                    finalCart.AddressLine1 = cart.Order.AddressLine1;
                    finalCart.AddressLine2 = cart.Order.AddressLine2;
                    finalCart.State = cart.Order.State;
                    finalCart.City = cart.Order.City;
                    finalCart.Pincode = cart.Order.Pincode;
                    finalCart.Date = cart.Order.Date;
                    finalCart.Total = cart.Order.Total;
                    finalCart.Name = cart.Order.Name;
                    finalCart.Phone = cart.Order.Phone;
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
            var sql = "INSERT INTO Orders (Name,UserID,StatusID,AddressLine1,AddressLine2,City,State,Pincode,Date, Total) VALUES(@Name,@UserID, @StatusID,@AddressLine1,@AddressLine2,@City,@State,@Pincode,@Date,@Total);SELECT CAST(SCOPE_IDENTITY() as int)";
            var rowsAffected = await connection.QueryFirstOrDefaultAsync<int>(sql, order, transaction);
            return rowsAffected > 0 ? new ResponseBaseModel<int>() { Body = rowsAffected, Message = "Successfully Added Order", StatusCode = 0 } :
                new ResponseBaseModel<int>() { Body = -1, Message = "Failed to add order", StatusCode = 1 };
        }

        public async Task<ResponseBaseModel<int>> AddToCart(CartDbModel cart, SqlConnection connection, SqlTransaction transaction)
        {
            cart.DateFormat = cart.Date.ToString("yyyy-MM-ddTHH:mm:ss");
            var sql = "INSERT INTO Cart (OrderID,StatusID,ServiceCategoryId,Rate, Quantity, Date) VALUES(@OrderID, @StatusID,@ServiceCategoryId,@Rate,@Quantity,@DateFormat);SELECT CAST(SCOPE_IDENTITY() as int)";
            var rowsAffected = await connection.QueryFirstOrDefaultAsync<int>(sql, cart, transaction);
            return rowsAffected > 0 ? new ResponseBaseModel<int>() { Body = rowsAffected, Message = "Successfully Added to cart", StatusCode = 0 } :
                new ResponseBaseModel<int>() { Body = -1, Message = "Failed to add cart", StatusCode = 1 };
        }

        public async Task<ResponseBaseModel<int>> CancelOrder(int cartId)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var parameters = new { cartId = cartId };

                //var userDto = new UserDto { Name = user.Name, Phone = user.Phone, Email = user.Email, ReceiveCommunication = user.ReceiveCommunication, Point = user.Point };
                var sql = "Update Cart SET StatusID = 3 where CartID = @cartId";
                var idOfNewRow = await connection.QueryFirstOrDefaultAsync<int>(sql, parameters);
                return idOfNewRow == 1 ? new ResponseBaseModel<int>() { Body = idOfNewRow, Message = "Successfully Cancelled the order", StatusCode = 0 } :
                    new ResponseBaseModel<int>() { Body = -1, Message = "Failed to cancel the order", StatusCode = 1 };
            }
        }

        public async Task<ResponseBaseModel<int>> ModifyCart(int cartId, DateTime dateTime)
        {
            using (var connection = new SqlConnection(_appSettings.DatabaseConnection))
            {
                var parameters = new { cartId = cartId, dateTime = dateTime.ToString("yyyy-MM-ddTHH:mm:ss") };
                var sql = "Update Cart SET Date = @dateTime where CartID = @cartId";
                var idOfNewRow = await connection.QueryFirstOrDefaultAsync<int>(sql, parameters);
                return idOfNewRow == 1 ? new ResponseBaseModel<int>() { Body = idOfNewRow, Message = "Successfully Modified the order", StatusCode = 0 } :
                    new ResponseBaseModel<int>() { Body = -1, Message = "Failed to modify the order", StatusCode = 1 };
            }
        }
    }
}
