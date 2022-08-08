using ServeMe.Models;
using ServeMe.Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ServeMe.Repository
{
    public interface IOrderRepository
    {
        Task<ResponseBaseModel<IEnumerable<OrderDto>>> GetOrdersByUser(int id);

        Task<ResponseBaseModel<IEnumerable<CartDto>>> GetOrdersByVendor(int id);

        Task<ResponseBaseModel<int>> PlaceOrder(OrderDbModel order, SqlConnection connection, SqlTransaction transaction);

        Task<ResponseBaseModel<int>> AddToCart(CartDbModel cart, SqlConnection connection, SqlTransaction transaction);

        Task<ResponseBaseModel<int>> CancelOrder(int cartId);
        Task<ResponseBaseModel<int>> ModifyCart(int cartId, DateTime dateTime);

        Task<ResponseBaseModel<int>> ConfirmBid(BidDto bidDto);

        Task<ResponseBaseModel<int>> MarkOrderComplete(CartDto cartDto);
    }
}
