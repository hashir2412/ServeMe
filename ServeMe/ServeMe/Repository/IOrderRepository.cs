using ServeMe.Models;
using ServeMe.Repository.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ServeMe.Repository
{
    public interface IOrderRepository
    {
        Task<ResponseBaseModel<IEnumerable<OrderDto>>> GetOrdersByUser(int id);

        Task<ResponseBaseModel<int>> PlaceOrder(OrderDbModel order, SqlConnection connection, SqlTransaction transaction);

        Task<ResponseBaseModel<int>> AddToCart(CartDbModel cart, SqlConnection connection, SqlTransaction transaction);
    }
}
