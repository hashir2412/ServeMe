﻿using Microsoft.Extensions.Options;
using ServeMe.Models;
using ServeMe.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ServeMe.Domain
{
    public class OrderDomain : IOrderDomain
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        public OrderDomain(IOrderRepository userRepository, IPaymentRepository credsRepository, IOptions<AppSettings> appSettings, IUserRepository userRepository1)
        {
            _orderRepository = userRepository;
            _paymentRepository = credsRepository;
            _appSettings = appSettings.Value;
            _userRepository = userRepository1;
        }
        public async Task<ResponseBaseModel<IEnumerable<OrderDto>>> GetOrdersByUser(int id)
        {
            return await _orderRepository.GetOrdersByUser(id);
        }

        public async Task<ResponseBaseModel<int>> PlaceOrder(OrderRequestModel order)
        {
            using (var conn = new SqlConnection(_appSettings.DatabaseConnection))
            {
                if (order.UserID == 0)
                {
                    var userExist = await _userRepository.GetUserDetails(order.Email);
                    if (userExist.StatusCode != 0 && userExist.Message == "User not found")
                    {
                        var user = await _userRepository.Register(new UserDto() { Email = order.Email });
                        if (user.StatusCode != 0)
                        {
                            return new ResponseBaseModel<int>() { Body = 1, Message = "Error placing order", StatusCode = 1 };
                        }
                        else
                        {
                            order.UserID = user.Body;
                        }
                    }
                    else
                    {
                        order.UserID = userExist.Body.UserId;
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
                            Address = order.Address,
                            Date = System.DateTime.Now,
                            UserID = order.UserID,
                            StatusID = 1,
                            Total = order.Total
                        }, conn, tran);
                        if (result.StatusCode == 0)
                        {
                            List<Task> tasks = new List<Task>();
                            foreach(var cart in order.Items)
                            {
                                tasks.Add(_orderRepository.AddToCart(new Repository.Models.CartDbModel()
                                {
                                    Date = cart.Date,
                                    OrderID = result.Body,
                                    Quantity = cart.Quantity,
                                    Rate = cart.Rate,
                                    StatusID = 1,
                                    ServiceID = cart.Service.ServiceID,
                                },conn, tran));
                            }
                            await Task.WhenAll(tasks);
                            var res = await _paymentRepository.AddPayment(new Repository.Models.PaymentDbModel()
                            {
                                UserID = order.UserID,
                                PaymentType = order.PaymentType,
                                OrderID = result.Body,
                                CommissionDeducted = 0.2 * order.Total,
                                TotalAmount = order.Total,
                                Date = System.DateTime.Now
                            }, conn, tran);
                            if (res.StatusCode == 0)
                            {
                                tran.Commit();
                                return new ResponseBaseModel<int>() { Body = 1, Message = "Successfully added Order", StatusCode = 0 };
                            }
                            else
                            {
                                tran.Rollback();
                                return new ResponseBaseModel<int>() { Body = 1, Message = "Error placing order", StatusCode = 1 };
                            }
                        }
                        // if it was successful, commit the transaction

                    }
                    catch (Exception ex)
                    {
                        // roll the transaction back
                        tran.Rollback();

                        // handle the error however you need to.
                        return new ResponseBaseModel<int>() { Body = 1, Message = "Error placing order", StatusCode = 1 };
                    }
                }
                return new ResponseBaseModel<int>() { Body = 1, Message = "Error placing order", StatusCode = 1 };

            }
        }
    }
}
