using Microsoft.Extensions.Options;
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
        private readonly AppSettings _appSettings;

        public OrderDomain(IOrderRepository userRepository, IPaymentRepository credsRepository, IOptions<AppSettings> appSettings)
        {
            _orderRepository = userRepository;
            _paymentRepository = credsRepository;
            _appSettings = appSettings.Value;
        }
        public async Task<ResponseBaseModel<IEnumerable<OrderDto>>> GetOrdersByUser(int id)
        {
            return await _orderRepository.GetOrdersByUser(id);
        }

        public async Task<ResponseBaseModel<int>> PlaceOrder(OrderRequestModel order)
        {
            using (var conn = new SqlConnection(_appSettings.DatabaseConnection))
            {

                // create the transaction
                // You could use `var` instead of `SqlTransaction`
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        var result = await _paymentRepository.AddPayment(new Repository.Models.PaymentDbModel()
                        {
                            UserID = order.UserID,
                            PaymentType = order.PaymentType
           ,
                            CommissionDeducted = 0.2 * order.Total,
                            TotalAmount = order.Total,
                            Date = System.DateTime.Now
                        }, conn);
                        if (result.StatusCode == 0)
                        {
                            var res = await _orderRepository.PlaceOrder(new Repository.Models.OrderDbModel()
                            {
                                Address = order.Address,
                                Date = System.DateTime.Now,
                                UserID = order.UserID,
                                StatusID = 1,
                                Total = order.Total
                            }, conn);
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
