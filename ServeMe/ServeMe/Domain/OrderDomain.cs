using Microsoft.Extensions.Options;
using ServeMe.Models;
using ServeMe.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ServeMe.Domain
{
    public class OrderDomain : IOrderDomain
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly AppSettings _appSettings;

        public OrderDomain(IOrderRepository userRepository, IPaymentRepository credsRepository, IOptions<AppSettings> appSettings, IUserRepository userRepository1, IVendorRepository vendorRepository)
        {
            _orderRepository = userRepository;
            _paymentRepository = credsRepository;
            _appSettings = appSettings.Value;
            _userRepository = userRepository1;
            _vendorRepository = vendorRepository;
        }

        public Task<ResponseBaseModel<int>> CancelCart(int cartId)
        {
            return _orderRepository.CancelOrder(cartId);
        }

        public Task<ResponseBaseModel<int>> ConfirmBid(BidDto bidDto)
        {
            return _orderRepository.ConfirmBid(bidDto);
        }

        public async Task<ResponseBaseModel<IEnumerable<OrderDto>>> GetOrdersByUser(int id)
        {
            var result = await _orderRepository.GetOrdersByUser(id);
            var vendors = await _vendorRepository.GetVendors();
            foreach (var order in result.Body)
            {
                foreach (var item in order.Items)
                {
                    foreach (var bid in item.Bids)
                    {
                        var vendor = vendors.Body.FirstOrDefault(vdr => vdr.VendorId == bid.VendorId);
                        bid.VendorName = vendor.Name;
                    }
                }
            }
            return result;
        }

        public async Task<ResponseBaseModel<IEnumerable<CartDto>>> GetOrdersByVendor(int id)
        {
            return await _orderRepository.GetOrdersByVendor(id);
        }

        public async Task<ResponseBaseModel<int>> ModifyCart(int cartId, DateTime dateTime)
        {
            return await _orderRepository.ModifyCart(cartId, dateTime);
        }

        public async Task<ResponseBaseModel<int>> PlaceOrder(OrderRequestModel order)
        {
            using (var conn = new SqlConnection(_appSettings.DatabaseConnection))
            {
                if (order.UserId == 0)
                {
                    var userExist = await _userRepository.GetUserDetails(order.Email);
                    if (userExist.StatusCode != 0 && userExist.Message == "User not found")
                    {
                        var user = await _userRepository.Register(new UserDto() { Email = order.Email, Phone = order.Phone });
                        if (user.StatusCode != 0)
                        {
                            return new ResponseBaseModel<int>() { Body = 1, Message = "Error placing order", StatusCode = 1 };
                        }
                        else
                        {
                            order.UserId = user.Body;
                        }
                    }
                    else
                    {
                        order.UserId = userExist.Body.UserId;
                    }
                }
                conn.Open();
                // create the transaction
                // You could use `var` instead of `SqlTransaction`
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        var result = await _orderRepository.PlaceOrder(new Repository.Models.OrderDbModel()
                        {
                            Name = order.Name,
                            AddressLine1 = order.AddressLine1,
                            AddressLine2 = order.AddressLine2,
                            City = order.City,
                            State = order.State,
                            Phone = order.Phone,
                            Pincode = order.Pincode,
                            Date = System.DateTime.Now,
                            UserID = order.UserId,
                            StatusID = 1,
                            Total = order.Total
                        }, conn, tran);
                        if (result.StatusCode == 0)
                        {
                            List<Task> tasks = new List<Task>();
                            foreach (var cart in order.Items)
                            {
                                tasks.Add(_orderRepository.AddToCart(new Repository.Models.CartDbModel()
                                {
                                    Date = cart.Date,
                                    OrderID = result.Body,
                                    Quantity = cart.Quantity,
                                    StatusID = 1,
                                    ServiceCategoryID = cart.ServiceCategoryId,
                                    AddressLine1 = order.AddressLine1,
                                    AddressLine2 = order.AddressLine2,
                                    City = order.City,
                                    State = order.State,
                                    Pincode = order.Pincode,
                                    Name = order.Name,
                                    Phone = order.Phone
                                }, conn, tran));
                            }
                            await Task.WhenAll(tasks);
                            //var res = await _paymentRepository.AddPayment(new Repository.Models.PaymentDbModel()
                            //{
                            //    UserID = order.UserId,
                            //    PaymentType = order.PaymentType,
                            //    OrderID = result.Body,
                            //    CommissionDeducted = 0.2 * order.Total,
                            //    TotalAmount = order.Total,
                            //    Date = System.DateTime.Now
                            //}, conn, tran);
                            //if (res.StatusCode == 0)
                            //{
                            tran.Commit();
                            return new ResponseBaseModel<int>() { Body = 1, Message = "Successfully added Order", StatusCode = 0 };
                            //}
                            //else
                            //{
                            //    tran.Rollback();
                            //    return new ResponseBaseModel<int>() { Body = 1, Message = "Error placing order", StatusCode = 1 };
                            //}
                        }
                        else
                        {
                            tran.Rollback();
                            return new ResponseBaseModel<int>() { Body = 1, Message = "Error placing order", StatusCode = 1 };
                        }
                        // if it was successful, commit the transaction

                    }
                    catch (Exception ex)
                    {
                        // roll the transaction back
                        tran.Rollback();

                        // handle the error however you need to.
                        return new ResponseBaseModel<int>() { Body = 1, Message = ex.Message, StatusCode = 1 };
                    }
                }

            }
        }
    }
}
