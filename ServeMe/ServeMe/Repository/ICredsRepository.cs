using ServeMe.Models;
using ServeMe.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServeMe.Repository
{
    public interface ICredsRepository
    {
        Task<bool> Register(string email, string password, bool isCustomer);

        Task<bool> UserExists(string email);

        Task<ResponseBaseModel<bool>> Login(string email, string password, bool isCustomer);
    }
}
