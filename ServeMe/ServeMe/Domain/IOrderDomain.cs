using ServeMe.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServeMe.Domain
{
    public interface IOrderDomain
    {
        Task<ResponseBaseModel<IEnumerable<OrderDto>>> GetOrdersByUser(int id);

        Task<ResponseBaseModel<int>> PlaceOrder(OrderRequestModel order);

        Task<ResponseBaseModel<int>> CancelCart(int cartId);

        Task<ResponseBaseModel<int>> ModifyCart(int cartId,DateTime dateTime);

    }
}
