using ServeMe.Models;
using System.Threading.Tasks;

namespace ServeMe.Domain
{
    public interface ILoginDomain
    {
        Task<ResponseBaseModel<BaseUserVendorDto>> Login(string email, string password, bool isCustomer);
    }
}
