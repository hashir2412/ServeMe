using ServeMe.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServeMe.Domain
{
    public interface IOrderDomain
    {
        Task<ResponseBaseModel<IEnumerable<OrderDto>>> GetOrdersByUser(int id);

        Task<ResponseBaseModel<int>> PlaceOrder(OrderRequestModel order);
    }
}
