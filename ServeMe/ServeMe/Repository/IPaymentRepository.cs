using ServeMe.Models;
using ServeMe.Repository.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ServeMe.Repository
{
    public interface IPaymentRepository
    {
        Task<ResponseBaseModel<int>> AddPayment(PaymentDbModel model, SqlConnection sqlConnection);
    }
}
